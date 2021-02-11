document.addEventListener('DOMContentLoaded', function() {
    var link = document.getElementById('do');
    // onClick's logic below:
    link.addEventListener('click', function() {       
        chrome.tabs.query({active: true, currentWindow: true}, (tabs) => {
            chrome.tabs.sendMessage(tabs[0].id, {type:"getText"}, (response) =>{
            });
        });
    });
});
