class User{
    id;name; password; isAdmin;
}
let tasks;
let token;

getToken=()=>{
    return sessionStorage.getItem("Token");
}

const getAll = () => {

    console.log(getToken());
    
    var myHeaders = new Headers();
    myHeaders.append("Authorization", "Bearer "+token);
    
    // var raw = "";
    
    var requestOptions = {
      method: 'GET',
      headers: myHeaders,
    //   body: raw,
      redirect: 'follow'
    };
    
    fetch("https://localhost:5001/ToDo", {
        method: 'GET',
        headers: {
            "Authorization": "Bearer "+getToken(),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    })
    .then(res=>res.json())
    .then(result=>{result.forEach(td => {
        const div = document.createElement('div');
        const br = document.createElement('hr');
        const p = document.createElement('p');
        p.innerHTML = 'To Do id:' + td.id;
        const h2 = document.createElement('h2');
        h2.innerHTML = 'To Do name:' + td.name;

        div.append(br, p, h2);

        document.getElementById('container').append(div);
    });
    })
    .catch(error => console.error('Unable to update item.', error));

           
        
}

getAll()
//add
const add = document.getElementById('add');
const toAdd = document.getElementById('toAdd');
add.onclick = () => {
    toAdd.style.visibility = 'initial';
}

const finalAdd = document.getElementById('finalAdd');
const inpname = document.getElementById('name');
// const name = document.getElementById('name');
finalAdd.onclick = () => {
    let td = { name: inpname.value, isDone: false };
    fetch("https://localhost:5001/ToDo", {
        method: 'POST',
        headers: {
            "Authorization": "Bearer "+token,
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(td)
    })
        .then(() => {
            getAll();
            inpname.value = '';
            toAdd.style.visibility = 'hidden';
        })
}


const del = document.getElementById('del');
const toDelete = document.getElementById('toDelete');
del.onclick = () => {
    toDelete.style.visibility = 'initial';
}
