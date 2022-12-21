const url ='/ToDo';
//getAll

function getAll() {
    document.getElementById('container').innerHTML='';
    let td = $.ajax({
        method: 'GET',
        url: url,
        success: (data) => {
            data.forEach(td => {
                const div = document.createElement('div');
                const br = document.createElement('hr');
                const p = document.createElement('p');
                p.innerHTML = 'To Do id:' + td.id;
                const h2 = document.createElement('h2');
                h2.innerHTML = 'To Do name:' + td.name;
                const h3 = document.createElement('h3');
                h3.innerHTML = 'To Do  is Done?:' + td.isDone;

                div.append(br, p, h2, h3);

                document.getElementById('container').append(div);
            });
        }
    })
}
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
    fetch(url, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(td)
    })
    .then(Response=>Response.json())
    .then(()=>{
        getAll();
        inpname.value='';
        toAdd.style.visibility = 'hidden';
    })
}


const del = document.getElementById('del');
const toDelete = document.getElementById('toDelete');
del.onclick = () => {
    toDelete.style.visibility = 'initial';
}

/////////
getAll();