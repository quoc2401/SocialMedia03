/* global pushSocket */
var ws;
$(function () {
    ws = new WebSocket(`wss://localhost:7155/ws/${UUID}`);
    
    ws.onmessage = function (event) {
        if (event.data === 'update_notif') {
            $('.list-notification').empty();
            notifPage = 1;
            getNotifs();
        }
        else
            console.log(event.data);
    };
    
    ws.onopen = function (event) {
        //send empty message to initialize socket connnection
        ws.send("");
    };

    ws.onclose = function (event) {
        ws.send(UUID);
        console.log(event.data);
    };
});




