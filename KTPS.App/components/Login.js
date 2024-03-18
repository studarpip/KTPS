import React, { useState, useEffect } from 'react';
import { Alert, ActivityIndicator, Pressable, SafeAreaView, StyleSheet, Switch, Text, TextInput, View, Modal, TouchableWithoutFeedback } from 'react-native';
import AsyncStorage from "@react-native-async-storage/async-storage";
import * as Constants from '../constants.js';

export default function LoginForm({ navigation }) {
  const [remember, setRemember] = useState(false);
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const [isEmailInputOpen, setIsEmailInputOpen] = useState(false);
  const [isPasswordResetAuthOpen, setIsPasswordResetAuthOpen] = useState(false);
  const [isPasswordResetOpen, setIsPasswordResetOpen] = useState(false);
  const [email, setEmail] = useState('');
  const [passwordResetAuth, setPasswordResetAuth] = useState('');
  const [user, setUser] = useState('');
  const [userAuth, setUserAuth] = useState('');
  const [userForReset, setUserForReset] = useState('');
  const [codeForReset, setCodeForReset] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [newPasswordConfirm, setNewPasswordConfirm] = useState('');
  const [isEmailSubmitDisabled, setIsEmailSubmitDisabled] = useState(false);
  const [isAuthSubmitDisabled, setIsAuthSubmitDisabled] = useState(false);
  const [isResetSubmitDisabled, setIsResetSubmitDisabled] = useState(false);

  useEffect(() => {
    checkLoggedIn();
  }, []);

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

  const checkLoggedIn = async () => {
    const userId = await AsyncStorage.getItem("userId")
    if (userId) {
      Alert.alert("Welcome back!");
      await AsyncStorage.removeItem('userId');
    }
  };

  const handleForgotPassword = async () => {
    setIsEmailInputOpen(true);
  }

  const handleResetSubmit = async () => {
    if (newPassword.trim() === '') {
      Alert.alert('Error', 'New password cannot be empty');
      return;
    }

    // if (!isStrongPassword(newPassword)) {
    //   Alert.alert(
    //     'Error',
    //     'Password must have at least 10 characters, contain at least one digit, one uppercase letter, and one special character.'
    //   );
    //   return;
    // }

    if (newPassword != newPasswordConfirm) {
      Alert.alert('Error', 'Passwords must match');
      return;
    }
    setIsResetSubmitDisabled(true);

    try {
      const response = await fetch(Constants.BASE_URL + '/login/reset', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          userID: userForReset,
          newPassword: newPassword,
          authCheck: codeForReset
        }),
      });

      const data = await response.json();
      if (data.success == true) {
        Alert.alert('Success', "Password has been changed!");
      } else {
        Alert.alert('Error', data.message);
      }
    } catch (error) {
      Alert.alert('Error', 'An error occurred. Please try again later.');
    } finally {
      setIsPasswordResetOpen(false);
      setIsResetSubmitDisabled(false);
      setPasswordResetAuth('');
      setNewPassword('');
      setNewPasswordConfirm('');
      setUserAuth('');
    }
  }

  const handleAuthSubmit = async () => {
    if (passwordResetAuth.trim() === '') {
      Alert.alert('Error', 'Recovery code cannot be empty.');
      return;
    }

    setIsAuthSubmitDisabled(true);

    try {
      const response = await fetch(Constants.BASE_URL + '/login/resetAuth', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          userID: userAuth,
          recoveryCode: passwordResetAuth
        }),
      });

      const data = await response.json();
      if (data.success == true) {
        setIsPasswordResetOpen(true);
        setCodeForReset(passwordResetAuth);
        setUserForReset(userAuth);
      } else {
        Alert.alert('Error', data.message);
      }
    } catch (error) {
      Alert.alert('Error', 'An error occurred. Please try again later.');
    } finally {
      setIsPasswordResetAuthOpen(false);
      setIsAuthSubmitDisabled(false);
      setPasswordResetAuth('');
    }
  }

  const handleEmailSubmit = async () => {
    if (email.trim() === '') {
      Alert.alert('Error', 'Email cannot be empty.');
      return;
    }

    // if (!isValidEmail(email)) {
    //   Alert.alert('Error', 'Please enter a valid email address.');
    //   return;
    // }

    setIsEmailSubmitDisabled(true);

    try {
      const response = await fetch(Constants.BASE_URL + '/login/forgotMyPassword', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          email: email
        }),
      });

      const data = await response.json();
      if (data.success == true) {
        setIsPasswordResetAuthOpen(true);
        setUserAuth(data.data);
      } else {
        Alert.alert('Error', data.message);
      }
    } catch (error) {
      Alert.alert('Error', 'An error occurred. Please try again later.');
    } finally {
      setIsEmailInputOpen(false);
      setIsEmailSubmitDisabled(false);
      setEmail('');
    }
  }

  const handleLogin = async () => {
    if (username.trim() === '' || password.trim() === '') {
      Alert.alert('Error', 'Username and password cannot be empty.');
      return;
    }

    setIsLoading(true);

    try {
      const response = await fetch(Constants.BASE_URL + '/login/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          username: username,
          password: password,
        }),
      });

      const data = await response.json();
      if (data.success == true) {
        Alert.alert('Success', 'Login successful!');
        if (remember)
          await AsyncStorage.setItem('userId', data.data.toString());
        setUser(data.data);
      } else {
        Alert.alert('Error', data.message);
      }
    } catch (error) {
      Alert.alert('Error', 'An error occurred. Please try again later.');
    } finally {
      setIsLoading(false);
      setPassword('');
      setUsername('');
    }
  };

  return (
    <SafeAreaView style={styles.container}>

      <View style={styles.inputView}>
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
      </View>
      <View style={styles.rememberView}>
        <View style={styles.switch}>
          <Switch
            style={styles.switchElement}
            value={remember}
            onValueChange={setRemember}
            trackColor={{ true: 'green', false: 'gray' }}
          />
          <Text style={styles.rememberText}>Remember Me</Text>
        </View>
        <View>
          <Pressable onPress={() => handleForgotPassword()}>
            <Text style={styles.forgetText}>Forgot Password?</Text>
          </Pressable>
        </View>
      </View>

      <View style={styles.buttonView}>
        <Pressable style={styles.button} onPress={() => handleLogin()}>
          <Text style={styles.buttonText}>Login</Text>
        </Pressable>
      </View>

      <Text style={styles.footerText}>
        Don't Have Account?{' '}
        <Pressable onPress={() => navigation.navigate('Register')}>
          <Text style={styles.signup}> Sign Up</Text>
        </Pressable>
      </Text>
      <Modal
        transparent={true}
        animationType='fade'
        visible={isLoading}
        onRequestClose={() => setIsLoading(false)}
      >
        <View style={[styles.modalContainer, { zIndex: 999 }]}>
          <ActivityIndicator size='large' color='red' />
        </View>
      </Modal>

      <Modal
        transparent={true}
        animationType='fade'
        visible={isEmailInputOpen}
        onRequestClose={() => { setIsEmailInputOpen(false); setEmail(''); }}
      >
        <TouchableWithoutFeedback onPress={() => { setIsEmailInputOpen(false); setEmail(''); }}>
          <View style={styles.modalContainer}>
            <TouchableWithoutFeedback>
              <View style={styles.emailModalContent}>
                <View style={styles.inputView}>
                  <TextInput
                    style={styles.emailInput}
                    placeholder='Enter your email'
                    placeholderTextColor='grey'
                    value={email}
                    onChangeText={setEmail}
                    autoCorrect={false}
                    autoCapitalize='none'
                  />
                  <Pressable
                    style={[styles.button, isEmailSubmitDisabled && styles.disabledButton]}
                    onPress={() => handleEmailSubmit()}
                    disabled={isEmailSubmitDisabled}
                  >
                    {isEmailSubmitDisabled ? (
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

      <Modal
        transparent={true}
        animationType='fade'
        visible={isPasswordResetAuthOpen}
        onRequestClose={() => { setIsPasswordResetAuthOpen(false), setPasswordResetAuth('') }}
      >
        <TouchableWithoutFeedback onPress={() => { setIsPasswordResetAuthOpen(false), setPasswordResetAuth('') }}>
          <View style={styles.modalContainer}>
            <TouchableWithoutFeedback>
              <View style={styles.emailModalContent}>
                <View style={styles.inputView}>
                  <TextInput
                    style={styles.emailInput}
                    placeholder='Enter recovery code'
                    placeholderTextColor='grey'
                    value={passwordResetAuth}
                    onChangeText={setPasswordResetAuth}
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

      <Modal
        transparent={true}
        animationType='fade'
        visible={isPasswordResetOpen}
        onRequestClose={() => { setIsPasswordResetOpen(false), setNewPassword('') }}
      >
        <TouchableWithoutFeedback onPress={() => { setIsPasswordResetOpen(false), setNewPassword('') }}>
          <View style={styles.modalContainer}>
            <TouchableWithoutFeedback>
              <View style={styles.emailModalContent}>
                <View style={styles.inputView}>
                  <TextInput
                    style={styles.emailInput}
                    placeholder='Enter new password'
                    secureTextEntry
                    placeholderTextColor='grey'
                    value={newPassword}
                    onChangeText={setNewPassword}
                    autoCorrect={false}
                    autoCapitalize='none'
                  />
                  <TextInput
                    style={styles.emailInputBottom}
                    placeholder='Confirm your password'
                    secureTextEntry
                    placeholderTextColor='grey'
                    value={newPasswordConfirm}
                    onChangeText={setNewPasswordConfirm}
                    autoCorrect={false}
                    autoCapitalize='none'
                  />
                  <Pressable
                    style={[styles.button, isResetSubmitDisabled && styles.disabledButton]}
                    onPress={() => handleResetSubmit()}
                    disabled={isResetSubmitDisabled}
                  >
                    {isResetSubmitDisabled ? (
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
  rememberView: {
    width: '100%',
    paddingHorizontal: 50,
    justifyContent: 'space-between',
    alignItems: 'center',
    flexDirection: 'row',
    marginBottom: 25,
  },
  switch: {
    flexDirection: 'row',
    gap: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  switchElement: {
    transform: [{ scaleX: 0.8 }, { scaleY: 0.8 }],
  },
  rememberText: {
    fontSize: 13,
  },
  forgetText: {
    fontSize: 13,
    color: 'red',
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
  footerText: {
    textAlign: 'center',
    color: 'gray',
  },
  signup: {
    color: 'red',
    fontSize: 13,
  },
  modalContainer: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: 'rgba(0, 0, 0, 0.5)',
  },
  emailModalContent: {
    backgroundColor: 'white',
    padding: 10,
    borderRadius: 10,
    alignItems: 'center',
    justifyContent: 'center',
    width: '80%'
  },
  emailInput: {
    height: 40,
    width: '100%',
    paddingHorizontal: 20,
    borderColor: 'red',
    borderWidth: 1,
    borderRadius: 10,
    marginBottom: 5,
    marginTop: 20
  },
  emailInputBottom: {
    height: 40,
    width: '100%',
    paddingHorizontal: 20,
    borderColor: 'red',
    borderWidth: 1,
    borderRadius: 10,
    marginBottom: 5,
    marginTop: 0
  }
});
