HOW TO LAUNCH THE APP:

1. Start WebApi and your database (Make sure appsettings.json are correct) Default constrings: "Database": "server=127.0.0.1;port=3306;uid=root;pwd=;database=ktps2"

2. Launch frontend wiht expo: "npx expo start" in the KTPS.App folder

3. Launch ngrok in the same folder and create a tunnel to the API with commad: "ngrok http https://localhost:7000" you will get a link like so: "https://5179-2a00-809-301-ee65-f802-cb68-39c4-a3f0.ngrok-free.app/"

4. In KTPS.App constants change BASE_URL to the link you got from ngrok
