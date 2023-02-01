const signIn=document.getElementById('signIn');

signIn.onclick=()=> {
var item = {
    "name": document.getElementById('username').value,
    "password": document.getElementById('password').value
};

        fetch("https://localhost:5001/User/login", {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(item)
        })
        .then(res=>res.text())
        .then(res=>{
            saveToken(res.split('"')[1]);
            location.href="/user.html";
        })
        .catch(error => console.error('Unable to update item.', error));
}

saveToken=(token)=>{
    sessionStorage.setItem('Token', token);
}
