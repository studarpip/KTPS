import React, { useState } from 'react';
import { Alert, ActivityIndicator, Pressable, SafeAreaView, StyleSheet, Text, TextInput, View, Modal, TouchableWithoutFeedback } from 'react-native';
import AsyncStorage from "@react-native-async-storage/async-storage";
import * as Constants from '../constants.js';

export default function RegisterForm({ navigation }) {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [email, setEmail] = useState('');
    const [isRegisterButtonDisabled, setIsRegisterButtonDisabled] = useState(false);
    const [registrationId, setRegistrationId] = useState('');
    const [isRegisterAuthOpen, setIsRegisterAuthOpen] = useState(false);
    const [isAuthSubmitDisabled, setIsAuthSubmitDisabled] = useState(false);
    const [registerAuth, setRegisterAuth] = useState('');

    const isStrongPassword = (password) => {
        if (password.length < 10) {
            return false;
        }
        if (!/\d/.test(password)) {
            return false;
        }
        if (!/[A-Z]/.test(password)) {
            return false;
        }
        if (!/[!@#$%^&*(),.?":{}|<>]/.test(password)) {
            return false;
        }
        return true;
    };

    const isValidEmail = (email) => {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
    };

    const handleAuthSubmit = async () => {
        if (registerAuth.trim() === '') {
            Alert.alert('Error', 'Code cannot be empty!');
            return;
        }

        setIsAuthSubmitDisabled(true);

        try {
            const response = await fetch(Constants.BASE_URL + '/register/auth', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    RegistrationID: registrationId,
                    AuthCode: registerAuth,
                }),
            });

            const data = await response.json();
            if (data.success == true) {
                await AsyncStorage.setItem('userId', data.data.toString());
                Alert.alert('Success', 'Succesfully registered!')
                navigation.navigate('Login');
            } else {
                Alert.alert('Error', data.message);
            }
        } catch (error) {
            Alert.alert('Error', 'An error occurred. Please try again later.');
        } finally {
            setIsAuthSubmitDisabled(false);
            setIsRegisterAuthOpen(false);
            setRegisterAuth('');
            setRegistrationId('');
        }
    }

    const handleRegister = async () => {
        if (username.trim() === '' || password.trim() === '' || email.trim() === '') {
            Alert.alert('Error', 'Email, username and password cannot be empty.');
            return;
        }

        // if (!isValidEmail(email)) {
        //     Alert.alert('Error', 'Please enter a valid email address.');
        //     return;
        // }

        // if (!isStrongPassword(password)) {
        //     Alert.alert(
        //         'Error',
        //         'Password must have at least 10 characters, contain at least one digit, one uppercase letter, and one special character.'
        //     );
        //     return;
        // }

        if (password != confirmPassword) {
            Alert.alert('Error', 'Passwords must match');
            return;
        }

        setIsRegisterButtonDisabled(true);

        try {
            const response = await fetch(Constants.BASE_URL + '/register/start', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    username: username,
                    email: email,
                    password: password,
                }),
            });

            const data = await response.json();
            if (data.success == true) {
                setRegistrationId(data.data);
                setIsRegisterAuthOpen(true);
            } else {
                Alert.alert('Error', data.message);
            }
        } catch (error) {
            Alert.alert('Error', 'An error occurred. Please try again later.');
        } finally {
            setIsRegisterButtonDisabled(false);
            setEmail('');
            setUsername('');
            setPassword('');
            setConfirmPassword('');
        }
    }

    return (
        <SafeAreaView style={styles.container}>
            <View style={styles.inputView}>
                <TextInput
                    style={styles.input}
                    placeholder='Email'
                    value={email}
                    onChangeText={setEmail}
                    autoCorrect={false}
                    autoCapitalize='none'
                />
                <TextInput
                    style={styles.input}
                    placeholder='Username'
                    value={username}
                    onChangeText={setUsername}
                    autoCorrect={false}
                    autoCapitalize='none'
                />
                <TextInput
                    style={styles.input}
                    placeholder='Password'
                    secureTextEntry
                    value={password}
                    onChangeText={setPassword}
                    autoCorrect={false}
                    autoCapitalize='none'
                />
                <TextInput
                    style={styles.input}
                    placeholder='Confirm your password'
                    secureTextEntry
                    value={confirmPassword}
                    onChangeText={setConfirmPassword}
                    autoCorrect={false}
                    autoCapitalize='none'
                />
            </View>
            <View style={styles.buttonView}>
                <Pressable
                    style={[styles.button, isRegisterButtonDisabled && styles.disabledButton]}
                    onPress={() => handleRegister()}
                    disabled={isRegisterButtonDisabled}
                >
                    {isRegisterButtonDisabled ? (
                        <ActivityIndicator size="small" color="#ffffff" />
                    ) : (
                        <Text style={styles.buttonText}>Register</Text>
                    )}
                </Pressable>
            </View>

            <Modal
                transparent={true}
                animationType='fade'
                visible={isRegisterAuthOpen}
                onRequestClose={() => { setIsRegisterAuthOpen(false), setRegistrationId('') }}
            >
                <TouchableWithoutFeedback onPress={() => { setIsRegisterAuthOpen(false), setRegistrationId('') }}>
                    <View style={styles.modalContainer}>
                        <TouchableWithoutFeedback>
                            <View style={styles.authModalContent}>
                                <View style={styles.inputView}>
                                    <TextInput
                                        style={styles.authInput}
                                        placeholder='Enter received code!'
                                        placeholderTextColor='grey'
                                        value={registerAuth}
                                        onChangeText={setRegisterAuth}
                                        autoCorrect={false}
                                        autoCapitalize='none'
                                    />
                                    <Pressable
                                        style={[styles.button, isAuthSubmitDisabled && styles.disabledButton]}
                                        onPress={() => handleAuthSubmit()}
                                        disabled={isAuthSubmitDisabled}
                                    >
                                        {isAuthSubmitDisabled ? (
                                            <ActivityIndicator size="small" color="#ffffff" />
                                        ) : (
                                            <Text style={styles.buttonText}>Submit</Text>
                                        )}
                                    </Pressable>
                                </View>
                            </View>
                        </TouchableWithoutFeedback>
                    </View>
                </TouchableWithoutFeedback>
            </Modal>
        </SafeAreaView>
    );
}

const styles = StyleSheet.create({
    container: {
        alignItems: 'center',
        flex: 1,
        justifyContent: 'center',
    },
    inputView: {
        gap: 20,
        width: '100%',
        paddingHorizontal: 40,
        marginBottom: 25,
    },
    input: {
        height: 40,
        paddingHorizontal: 20,
        borderColor: 'red',
        borderWidth: 1,
        borderRadius: 10,
    },
    button: {
        backgroundColor: 'red',
        height: 45,
        borderColor: 'gray',
        borderWidth: 1,
        borderRadius: 25,
        alignItems: 'center',
        justifyContent: 'center',
    },
    buttonText: {
        color: 'white',
        fontSize: 18,
        fontWeight: 'bold',
    },
    buttonView: {
        width: '100%',
        paddingHorizontal: 50,
        marginBottom: 25,
    },
    modalContainer: {
        flex: 1,
        alignItems: 'center',
        justifyContent: 'center',
        backgroundColor: 'rgba(0, 0, 0, 0.5)',
    },
    authModalContent: {
        backgroundColor: 'white',
        padding: 10,
        borderRadius: 10,
        alignItems: 'center',
        justifyContent: 'center',
        width: '80%'
    },
    authInput: {
        height: 40,
        width: '100%',
        paddingHorizontal: 20,
        borderColor: 'red',
        borderWidth: 1,
        borderRadius: 10,
        marginBottom: 5,
        marginTop: 20
    },
});
