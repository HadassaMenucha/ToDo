class user{
    id;name; password; isAdmin;
}
class ToDo{
    id; userid; name; isdone;
}
let tasks;
let token;

getToken=()=>{
    return sessionStorage.getItem("Token");
}

const getAll = () => {
    document.getElementById('container').innerHTML="";
    
    fetch("https://localhost:5001/User", {
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
        const p2=document.createElement('p')
        p.innerHTML = 'User Id:' + td.id;
        const h2 = document.createElement('h2');
        h2.innerHTML = 'User name:' + td.name;
        p2.innerHTML='is Admin? '+ td.isAdmin;


        div.append(br, p, h2,p2);

        document.getElementById('container').append(div);
    });
    })
    .catch(error =>{alert('You are not a administrator, you don\'t have permission');});

           
        
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
const pass=document.getElementById('pass');
const isAdmin=document.getElementById('admin');
finalAdd.onclick = () => {
    let us =new user();
    us.name=inpname.value;
    us.password=pass.value;
    us.isAdmin=isAdmin.checked;

    fetch("https://localhost:5001/User", {
        method: 'POST',
        headers: {
            "Authorization": "Bearer "+getToken(),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(us)
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

const finalDelete=document.getElementById('finalDelete');
finalDelete.onclick=()=>{
    const id= document.getElementById('id').value;

    fetch("https://localhost:5001/User?id="+id, {
        method: 'DELETE',
        headers: {
            "Authorization": "Bearer "+getToken(),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: id
    })
        .then(() => {
            getAll();
            id.value = '';
            toDelete.style.visibility = 'hidden';
        })

}

// const update =document.getElementById('update');
// const toupdate= document.getElementById('toUpdate')
// update.onclick=()=>{
//     toupdate.style.visibility='initial';
// }
// const finalUpdate=document.getElementById('finalUpdate');
// const updateid=document.getElementById('updateId');
// const updatename=document.getElementById('taskName');
// const isdone=document.getElementById('isDone');
// finalUpdate.onclick=()=>{
//     let td= new ToDo();
//     td.id=updateid.value;
//     td.name=updatename.value;
//     td.isdone=isdone.value;

//     fetch("https://localhost:5001/User", {
//         method: 'PUT',
//         headers: {
//             "Authorization": "Bearer "+getToken(),
//             'Accept': 'application/json',
//             'Content-Type': 'application/json'
//         },
//         body: JSON.stringify(td)
//     })
//         .then(() => {
//             getAll();
//             updateid.value = '';
//             updatename.value = '';
//             isdone.value = '';
//             toupdate.style.visibility = 'hidden';
//         })
// }