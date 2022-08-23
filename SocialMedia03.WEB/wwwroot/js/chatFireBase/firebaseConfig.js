
import "https://www.gstatic.com/firebasejs/8.10.0/firebase.js";

const firebaseConfig = {
    apiKey: "AIzaSyBrkgEivhiL2-0HdcU8aTSHwGpRohWQFgA",
    authDomain: "socialmedia03-76bf8.firebaseapp.com",
    databaseURL: "https://socialmedia03-76bf8-default-rtdb.firebaseio.com",
    projectId: "socialmedia03-76bf8",
    storageBucket: "socialmedia03-76bf8.appspot.com",
    messagingSenderId: "703361284714",
    appId: "1:703361284714:web:5d6e968a66296c91466813"
};

firebase.initializeApp(firebaseConfig);
export const db = firebase.database();
