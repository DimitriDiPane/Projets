var xhr = new XMLHttpRequest();
var chanName = "joueur_du_grenier"
xhr.open("GET", "https://api.twitch.tv/helix/streams?user_login="+ chanName, true);
xhr.setRequestHeader("Authorization","Bearer " + bearer);
xhr.setRequestHeader("Client-Id",clientId);
xhr.onreadystatechange = function(){
    if(xhr.readyState == 4){
        var data = JSON.parse(xhr.responseText);
        if(data["data"] == ""){
            $('#info').html(chanName + " n'est pas en live pour l'instant.");
            $('#info').css({"color":"red"});
            chrome.browserAction.setIcon({path:"img/icon128off.png"});
        }else{
            $('#info').html(chanName + " est en live sur le jeu " + String(data["data"][0]["game_name"]) +" Viens vite!");
            $('#info').css({"color":"green"});
            chrome.browserAction.setIcon({path:"img/icon128on.png"});            
        }
    }
} 
xhr.send();

$('#click').on("click",function(){
    chrome.tabs.create({ url: "https://www.twitch.tv/" + chanName})
});

$('#notif').on("click",function(){
    if($('#notif').prop("checked") == true){
        localStorage.setItem("notif",true);
    };
    if($('#notif').prop("checked") == false){
        localStorage.setItem("notif",false);
    };
});

$(document).ready(function() {
    var notif = new Boolean();
    if(localStorage.getItem("notif") == "true"){
        notif = true;
    }else{
        notif = false;
    }
    if(notif){       
        $('#notif').prop("checked",true);
    };
    if(!notif){
        $('#notif').prop("checked", false);
    };
});