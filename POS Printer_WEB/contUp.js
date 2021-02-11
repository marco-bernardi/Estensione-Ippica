let text = "hello";
console.log("POS Printer Log");
chrome.runtime.onMessage.addListener((message, sender, sendResponse) => {
    var quota;
    var importo;
    var vincita;
    var corsa;
    var biglietto;
    var itemEvent = new Array();

    // MAINMULTIPLA NORMALE QUOTA FISSA
    // MAINSINGOLE  TOTALIZZATORE
    // MAINSISTEMA  SISTEMA TOTALIZZATORE
    var MainMultipla = document.getElementById('MainMultipla');
    var MainSingole = document.getElementById('MainSingole');
    var MainSistemi = document.getElementById('MainSistema');  
    if (MainMultipla != null && MainMultipla.style.display == "block"){
        var idEvent = document.getElementById('esitoBetM').innerText.split('\n');
        if (idEvent != null){
            biglietto = idEvent[1].split(':')[1].trim();
            //console.log(biglietto);
        }  
        //console.log("Si vede Multipla");
        var i = 0;
        while (i < 100){
            i++;
            var temp = document.getElementById('R'+i);
            if (temp != null){
                itemEvent.push(temp);
                //console.log(temp.innerText)
            }
        }
        var qTotM = document.getElementById('qTotM');
        if (qTotM != null){
            quota = qTotM.innerText;
            //console.log(quota);
        }
        var txtb2 = document.getElementById('TextBox2');
        if (txtb2 != null){
            importo = txtb2.value;
            //console.log(importo);
        }
        var winPotm = document.getElementById('winPotM');
        if (winPotm != null){
            vincita = winPotm.innerText;
            //console.log(vincita);
        }
        if (quota != null && importo != null && vincita != null && biglietto != null && itemEvent.length != 0){
            var complete = "QF*" + biglietto + "*" + quota + "*" + vincita + "*" + importo + "*";
            itemEvent.forEach(function (item, index, array) {
                var split = item.innerText.split('\n');
                complete += split[0] + "*" + split[1] + "*" + split[2] + "*" ;
            });
            //console.log("Complete: " + complete);
            var stamp = "print://" + complete;
            window.open(stamp);
        } 
    } else if (MainSingole != null && MainSingole.style.display == "block"){
        //console.log("Si vede Singole");
        var idEvent = document.getElementById('esitoBet').innerText.split('\n');
        if (idEvent != null){
            biglietto = idEvent[1].split(':')[1].trim();
            //console.log(biglietto);
        }  
        var evento = MainSingole.querySelector("#PCavv").innerText;
        evento = evento.normalize("NFD").replace(/[\u0300-\u036f]/g, "");
        var info = MainSingole.getElementsByClassName('MainTicketPronostico')[0];
        //console.log(info);
        var spl = info.innerText.split('\n');
        var data = spl[0];
        corsa = spl[1];
        var scommessa = spl[2];
        var pronostico = spl[3]
        var txtb1 = document.getElementById('TextBox1');
        if (txtb1 != null){
            importo = txtb1.value;
            //console.log(importo);
        }
        if (biglietto != null && evento != null && data != null && corsa != null && scommessa != null && pronostico != null && importo != null){
            var complete = "TSING*" + biglietto + "*" + evento + "*" + data + "*" + corsa + "*" + scommessa + "*"+ pronostico + "*" + importo;
            //console.log("Complete: " + complete);
            var stamp = "print://" + complete;
            window.open(stamp);
        } 

    } else if (MainSistemi != null && MainSistemi.style.display == "block"){
        //console.log("Si vede Sistemi");
        //console.log("Si vede Singole");
        var idEvent = document.getElementById('esitoBetS').innerText.split('\n');
        if (idEvent != null){
            biglietto = idEvent[1].split(':')[1].trim();
            //console.log(biglietto);
        }
        window.open("https://www.replatz.it/Ippica/Scommetti.aspx?ticketId=" + biglietto);
    } else if (window.location.href.includes("ticketId=")){
        var frame = document.getElementById('IppicaFrame');
        var data;
        var tipo;
        if (frame != null){
            
            var container = frame.contentWindow.document.querySelector("#mainTicket");
            if (container != null){
                var splitted = container.getElementsByClassName('ticketHeader')[0].innerText.split('\n');
                biglietto = splitted[0].split(':')[1].trim();
                //console.log(biglietto);
                tipo = splitted[3].split(':')[1].trim();
                //console.log(tipo);
                data = splitted[5].split(':')[1].trim()
                //console.log(data);
            }
            var importo = container.getElementsByClassName('ticketFooter')[0].getElementsByClassName('right')[0].innerText.split('\n')[1].replace('€ ', '');
            //console.log(importo);
            var rowT = container.querySelectorAll('tr');
            var complete;
            if (biglietto != null && importo != null && rowT != null){
                complete = "TSIS*" + biglietto + "*" + importo + "*" 
                for (let index = 1; index < rowT.length; index++) {
                    var cells = rowT[index].querySelectorAll('td').forEach(x => complete += x.innerText.replace('€ ', '') + "*");
                    
                }
            }
            var stamp = "print://" + complete;
            //console.log(complete);
            window.open(stamp);
            

        }
    }
});    
