notif();
checkStream();
setInterval(function(){
    checkStream();
}, 600000);

function notif(){
    var notif = localStorage.getItem("notif");
    if(notif == null){
        localStorage.setItem("notif",true);
        notif = localStorage.getItem("notif");
    }
}

var popup = false;
var chanName = "joueur_du_grenier";

function checkStream(){
    var xhr = new XMLHttpRequest();
xhr.open("GET", "https://api.twitch.tv/helix/streams?user_login=" + chanName, true);
xhr.setRequestHeader("Authorization","Bearer " + bearer);
xhr.setRequestHeader("Client-Id",clientId);

xhr.onreadystatechange = function(){
    if(xhr.readyState == 4){
        var data = JSON.parse(xhr.responseText);
        if(data["data"] == ""){
            $('#info').html(chanName + " n'est pas en live pour l'instant.");
            $('#info').css({"color":"red"});
            chrome.browserAction.setIcon({path:"img/icon128off.png"});
            popup = false;
        }else{
            $('#info').html(chanName + " est en live " + String(data["data"][0]["game_name"]) +" Viens vite!");
            $('#info').css({"color":"green"});
            chrome.browserAction.setIcon({path:"img/icon128on.png"});
            if(!popup){
                if(localStorage.getItem("notif") == "true"){
                    var notification = new Notification(chanName + ' a lancé un live sur le jeu '+ String(data["data"][0]["game_name"]) +'!',{
                        icon: "img/icon128on.png",
                        body: "Viens vite nous rejoindre!"
                    });                    
                }
                popup = true;
            }
        }
    }
} 
xhr.send();

}