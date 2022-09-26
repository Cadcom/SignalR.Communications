// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var connection;
var curRow = 0;
$(document).ready(() => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:4000/api/gateway/hub/ProcessHub/", {
            //skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets,
            accessTokenFactory: () => localStorage.getItem("accessToken") != null ? localStorage.getItem("accessToken") : ""
        })
        .configureLogging(signalR.LogLevel.Information)
        .build();

    async function start() {
        try {
            await connection.start();
            console.log("SignalR Connected.");
        } catch (err) {
            console.log(err);
            setTimeout(start, 5000);
        }
    };

    connection.onclose(async () => {
        await start();
    });

    // Start the connection.
    start();


    connection.on("carsLoaded", function (data) {
        if (data != null)
            tableGanerator(JSON.parse(data));
    });

    connection.on("databaseRefreshed", function (data) {
        if (data == "ok")
            loadCars();
        document.getElementById("lblStatus").innerHTML = 'Car status updated!';
    });

    
    function tableGanerator(data) {
        var table = document.querySelector("#tblCars tbody");
        table.innerHTML="";

        var index = 0;
        data.forEach(function (rowData) { // For each row
            var row = '<tr>';
            row += `<td>${rowData.CarType}</td>`;           
            row += `<td><input type="checkbox" ${rowData.isLeftDoorOpen ? "checked" : ""}></td>`;
            row += `<td><input type="checkbox" ${rowData.isRightDoorOpen ? "checked" : ""}></td>`;
            row += `<td><button class="btn btn-secondary" onclick="curRow=${index};updateCar(${rowData.ID},'left')">left Door</button></td>`;
            row += `<td><button class="btn btn-secondary" onclick="curRow=${index};updateCar(${rowData.ID},'right')">Right Door</button></td>`;
            row += `<td><button class="btn btn-secondary" onclick="curRow=${index};purchaseCar(${rowData.ID})">Purchase</button></td>`;
            row += '</tr>';

            index++
            table.innerHTML += row;
        })
    }


    connection.on("purchaseList", function (data) {
        if (data != null)
            purchaseTableGanerator(JSON.parse(data));
    });

    connection.on("carUpdated", function (data) {
        if (data != null) {
            var rows = document.querySelector("#tblCars tbody").querySelectorAll("tr");
            var car = JSON.parse(data);

            rows[curRow].childNodes[1].childNodes[0].checked = car.isLeftDoorOpen;
            rows[curRow].childNodes[2].childNodes[0].checked = car.isRightDoorOpen;

            document.getElementById("lblStatus").innerHTML = 'Car status updated!';
        }
            
    });

    connection.on("carPurchased", function (data) {
        if (data != null) {
            
            var rows = document.querySelector("#tblCars tbody").querySelectorAll("tr");
            var car = rows[curRow].childNodes[0].innerHTML;
            document.getElementById("lblStatus").innerHTML = `${car} purchased!`;
        }

    });

    

});

function btnClick() {


    var userName = document.getElementById("txtUserName");
    var password = document.getElementById("txtPassword");
    var spans = document.getElementsByTagName("span");

    var url = `https://localhost:4000/api/gateway/Auths/Login/${userName.value}/${password.value}`;

    var jqxhr = $.get(url, function (token, status, e) {
        if (status='success') {
            spans[1].innerHTML = token.accessToken;
            localStorage.setItem("accessToken", token.accessToken);
            localStorage.setItem("refreshToken", token.refreshToken);
            setTimeout(() => { }, 2000);
            window.location.href = '/cars';
        }
        else {
            spans[1].innerHTML = "Incorrect username or password";
        }
    })
        
        .fail(function (status) {
            alert("thrown error");
        });
        
} 

function loadCars() {
    //console.log(connection.state);
    connection.invoke("listCars").catch(error => console.log(`Hata :  ${error}`));

}


function updateCar(id, door) {
    var rows = document.querySelector("#tblCars tbody").querySelectorAll("tr");
    var rowData = rows[curRow].querySelectorAll("td");

    document.getElementById("lblStatus").innerHTML = '';
    var leftDoor = rowData[1].childNodes[0].checked == true;
    var rightDoor = rowData[2].childNodes[0].checked == true;

    if (door == 'left') leftDoor = !leftDoor;
    if (door == 'right') rightDoor = !rightDoor;

    var json = `{ "CarID": ${id}, "isLeftDoorOpen": ${leftDoor}, "isRightDoorOpen": ${rightDoor}}`;

    connection.invoke("processCar", json).catch(error => console.log(`Hata :  ${error}`));
    
}

function purchaseCar(id) {
    connection.invoke("purchaseCar", id).catch(error => console.log(`Hata :  ${error}`));
}


function listPurchases() {
    //console.log(connection.state);
    connection.invoke("listPurchaseCars").catch(error => console.log(`Hata :  ${error}`));

}

function purchaseTableGanerator(data) {
    var table = document.querySelector("#tblPurchases tbody");
    table.innerHTML = "";

    var index = 0;
    data.forEach(function (rowData) { // For each row
        var row = '<tr>';
        row += `<td>${rowData.ID}</td>`;
        row += `<td>${new Date(rowData.ProcessDate).toLocaleString("tr-TR") }</td>`;
        row += `<td>${rowData.Car }</td>`;
        row += '</tr>';

        index++
        table.innerHTML += row;
    })
}