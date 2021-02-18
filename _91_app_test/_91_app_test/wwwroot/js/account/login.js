const uri = 'api/Account/Login';
var account;
var password;
var errorMsg;

window.onload = init;
function init() {
    account = document.getElementById("account");
    password = document.getElementById("password");
    errorMsg = document.getElementById("errorMessage");

}
function login() {
    if (account.value == '') {
        showErrorMsg("請輸入帳號");
        return;
    }
    if (password.value == '') {
        showErrorMsg("請輸入密碼");
        return;
    }
    postLogin(account.value, password.value);
}

function postLogin(account, password) {
    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ account: account, password: password })
    }
    )
        .then(response => response.json())
        .then(data => loginResult(data))
        .catch(error => showErrorMsg(error));
}

function loginResult(data) {
    if (data.code == 1) {
        console.log(data.data.account + data.data.password);
        localStorage.setItem('account', data.data.account);
        window.location.href = "/OrderList.html";
    } else {
        showErrorMsg(data.message);
    }
        
} 

function showErrorMsg(message) {
    errorMsg.textContent = message;
    errorMsg.style.display = "block";
}


