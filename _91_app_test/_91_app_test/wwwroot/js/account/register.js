const uri = "api/Account/Register";
var account;
var password;
var confirmPassword;
var errorMsg;


window.onload = init;

function init() {
    account = document.getElementById("account");
    password = document.getElementById("password");
    confirmPassword = document.getElementById("confirmPassword");
    errorMsg = document.getElementById("errorMessage");
}

function register()
{
    if(!checkData()) return;

    postRegister(account.value, password.value);
    console.log("Success");
}
function postRegister(acc, pass){

    console.log(JSON.stringify({ account: acc, password: pass }));
    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ account: acc, password: pass })
    })
        .then(response => response.json())
        .then(data => registerResult(data))
        .catch(error => showErrorMsg(error));
}

function registerResult(data) {
    if (data.code == 1) {
        console.log(data.data.account + data.data.password);
        localStorage.setItem('account', data.data.account);
        window.location.href = "/OrderList.html";
    } else {
        showErrorMsg(data.message);
    }
        
} 

function checkData(){
    if(account.value == false){
        showErrorMsg("請輸入帳號");
        return false;
    } 

    if(password.value == false){
        showErrorMsg("請輸入密碼");
        return false;
    } 

    if(confirmPassword.value != password.value){
        showErrorMsg("確認密碼錯誤");
        return false;
    } 
    return true;

}

function showErrorMsg(message) {
    errorMsg.textContent = message;
    errorMsg.style.display = "block";
}