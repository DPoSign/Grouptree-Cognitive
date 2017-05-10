/*
Name: Daniel Poinsignon
Date: 26/04/2017
Description: 
*/
document.addEventListener("DOMContentLoaded", getURL);
document.addEventListener("DOMContentLoaded", getIMAGE);

function getURL() {
    document.getElementById("urlSubmit").addEventListener("click", function(event) {
        var request = new XMLHttpRequest();
        var payload = {
            url: null
        };
        payload.url = document.getElementById("urlID").value;
        var imagecognitive = "https://westus.api.cognitive.microsoft.com/vision/v1.0/analyze?visualFeatures=Categories,Tags,Description&language=en";
        request.open("POST", imagecognitive, true);
        request.setRequestHeader("Content-Type", "application/json");
        request.setRequestHeader("Ocp-Apim-Subscription-Key", "88b44fb4b424469581e8e1ac1624d2e7");
        request.addEventListener("load", function() {
            if (request.status >= 200 && request.status < 400) {
                var response = JSON.parse(request.responseText);
                console.log(response);
                //TESTING
                str = JSON.stringify(request.responseText);
                str = JSON.stringify(request.responseText, null, 4); // (Optional) beautiful indented output.
                document.getElementById("myText").innerHTML = str;
                //TESTING
            } else {
                console.log("Error in network requestuest: " + request.statusText);
            }
        })
        request.send(JSON.stringify(payload));
        event.preventDefault();
    })
}

function getIMAGE() {
    document.getElementById("imageSubmit").addEventListener("click", function(event) {
        var request = new XMLHttpRequest();
        var payload = document.getElementById('imageID').files[0];
        var postURL = "https://westus.api.cognitive.microsoft.com/vision/v1.0/analyze?visualFeatures=Categories,Tags,Description&language=en";
        request.open("POST", postURL, true);
        request.setRequestHeader("Content-Type", "application/octet-stream");
        request.setRequestHeader("Ocp-Apim-Subscription-Key", "88b44fb4b424469581e8e1ac1624d2e7");
        request.addEventListener("load", function() {
            if (request.status >= 200 && request.status < 400) {
                var response = JSON.parse(request.responseText);
                console.log(response);
                //TESTING
                str = JSON.stringify(request.responseText, null, "\t"); // Indented with tab
                //str = JSON.replace(/\"([^(\")"]+)\":/g,"$1:");  
                document.getElementById("myText").innerHTML = str;
                //TESTING
            } else {
                console.log("Error in network requestuest: " + request.statusText);
            }
        })
        request.send(payload);
        event.preventDefault();
    })
}

function previewFile() { 
    var preview = document.querySelector('img'); 
    var file   = document.getElementById("imageID").files[0]; 
    var reader  = new FileReader(); 
    reader.addEventListener("load", function() {  
        preview.src = reader.result; 
    }, false); 
    if (file) {  
        reader.readAsDataURL(file); 
    }
}