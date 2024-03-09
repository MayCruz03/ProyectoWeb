const uri = 'api/tblRols';
let todos = [];
//Utilizar el metodo get de http
function getRol() {
    fetch(uri)
        .then(response => response.json())
        .then(data => MostrarRoles(data))
        .catch(error => console.error("No se logró cargar datos", error));
}
//Contar registros de la tabla
function MostrarContador(itemCount) {
    const name = (itemCount === 1) ? 'Registro' : 'Registros';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

//Método mostrar empleados
function MostrarRoles(data) {
    const tBody = document.getElementById('DetRol');
    tBody.innerHTML = '';

    MostrarContador(data.length);

    const button = document.createElement('button');
    data.forEach(item => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `redirectToEditPage(${item.rol_id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteEmple(${item.rol_id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let txtCode = document.createTextNode(item.rol_id);
        td1.appendChild(txtCode);

        let td2 = tr.insertCell(1);
        let txtDescripcion = document.createTextNode(item.rol_descripcion);
        td2.appendChild(txtDescripcion);

        let td3 = tr.insertCell(2);
        let txtObservaciones = document.createTextNode(item.rol_observaciones);
        td3.appendChild(txtObservaciones);

        let td4 = tr.insertCell(3);
        td4.appendChild(editButton);

        let td5 = tr.insertCell(4);
        td5.appendChild(deleteButton);
    });
    todos = data;
}