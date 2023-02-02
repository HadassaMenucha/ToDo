class User {
    id; name; password; isAdmin;
}
class ToDo {
    id; userid; name; isdone;
}
let tasks;
let token;

getToken = () => {
    return sessionStorage.getItem("Token");
}

const getAll = () => {
    document.getElementById('container').innerHTML = "";

    fetch("https://localhost:5001/ToDo", {
        method: 'GET',
        headers: {
            "Authorization": "Bearer " + getToken(),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    })
        .then(res => res.json())
        .then(result => {
            result.forEach(td => {
                const div = document.createElement('div');
                const br = document.createElement('hr');
                const p = document.createElement('p');
                const p2 = document.createElement('p')
                p.innerHTML = 'To Do id:' + td.id;
                const h2 = document.createElement('h2');
                h2.innerHTML = 'To Do name:' + td.name;
                p2.innerHTML = 'is done? ' + td.isDone;

                div.append(br, p, h2, p2);

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
finalAdd.onclick = () => {
    let td = new ToDo();
    td.name = inpname.value;
    td.isdone = false;
    fetch("https://localhost:5001/ToDo", {
        method: 'POST',
        headers: {
            "Authorization": "Bearer " + getToken(),
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

const finalDelete = document.getElementById('finalDelete');
finalDelete.onclick = () => {
    const id = document.getElementById('id').value;

    fetch("https://localhost:5001/ToDo", {
        method: 'DELETE',
        headers: {
            "Authorization": "Bearer " + getToken(),
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

const update = document.getElementById('update');
const toupdate = document.getElementById('toUpdate')
update.onclick = () => {
    toupdate.style.visibility = 'initial';
}
const finalUpdate = document.getElementById('finalUpdate');
const updateid = document.getElementById('updateId');
const updatename = document.getElementById('taskName');
const isdone = document.getElementById('isDone');
finalUpdate.onclick = () => {
    let td = {
        id: updateid.value,
        name: updatename.value,
        isdone: isdone.checked
    }


    console.log(td);

    fetch("https://localhost:5001/ToDo", {
        method: 'PUT',
        headers: {
            "Authorization": "Bearer " + getToken(),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(td)
    })
        .then(res =>{ getAll();
            updateid.value = '';
            updatename.value = '';
            isdone.value = '';
            toupdate.style.visibility = 'hidden';
        })
        .catch(err => { console.log(err); })
}


const certainToDo = document.getElementById('certainToDo');
const toCertainId = document.getElementById('toCertainId');
const taskid = document.getElementById('taskid');
const showCertain = document.getElementById('showCertain');

certainToDo.onclick = () => {
    toCertainId.style.visibility = 'initial';
}
showCertain.onclick = () => {
    id = taskid.value;

    fetch("https://localhost:5001/ToDo/" + id, {
        method: 'GET',
        headers: {
            "Authorization": "Bearer " + getToken(),
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        // body: JSON.stringify(td)
    })
        .then(res => res.json())
        .then((td) => {
            document.getElementById('showContainer').innerHTML = '';
            const div = document.createElement('div');
            const br = document.createElement('hr');
            const p = document.createElement('p');
            const p2 = document.createElement('p')
            p.innerHTML = 'To Do id:' + td.id;
            const h2 = document.createElement('h2');
            h2.innerHTML = 'To Do name:' + td.name;
            p2.innerHTML = 'is done? ' + td.isDone;

            div.append(br, p, h2, p2);

            document.getElementById('showContainer').append(div);
        }
        )
}