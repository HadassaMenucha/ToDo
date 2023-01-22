let token;
const signIn=document.getElementById('signIn');

signIn.onclick=()=> {
    var myHeaders = new Headers();
    myHeaders.append("Authorization", "Bearer "+token);
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify({
        "username": document.getElementById('username').value,
        "password": document.getElementById('password').value
    });

    var requestOptions = {
        method: 'PUT',
        headers: myHeaders,
        body: raw,
        redirect: 'follow'
    };

    fetch("https://localhost:5001/User/login", requestOptions)
        .then(response => response.text())
        .then(result => {token = result; console.log(token); location.href='/user.html'})
        .catch(error => console.log('error', error));
}
