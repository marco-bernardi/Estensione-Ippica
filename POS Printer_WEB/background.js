function Diocane(){
    chrome.tabs.executeScript(null, {
        code: "alert(document.querySelector('winPotM').innerText)"
    });
}