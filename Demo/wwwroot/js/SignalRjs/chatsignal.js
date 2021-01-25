var connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:44383/ServerHub")//调用统一个程序可去除https://localhost:44383 后端需要配置跨域
    .withAutomaticReconnect() //断线自动重连
    .build();
connection.serverTimeoutInMilliseconds = 24e4;
connection.keepAliveIntervalInMilliseconds = 12e4;

var button = document.getElementById("getValues");

///连接后发送信息
connection.start().then(() => {
    connection.invoke("RegisterUserMessage", "asdfasdf").catch(function (err) {
        return console.error(err.toString());
    });
});

//自动重连成功后的处理
connection.onreconnected(connectionId => {
    alert(connectionId);
});


//---发送信息---
function onSendChan() {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
};
///处理后端发送来的信息
connection.on("ReceiveMessage", function (res) {
    if (res.length > 0) {
        alert(res);
    }
});