const uri = 'api/tblUsers';
let todos = [];
//Utilizar el metodo get de http
function getUser() {
    fetch(uri)
        .then(response => response.json())
        .then(data => MostrarUsuarios(data))
        .catch(error => console.error("No se logró cargar datos", error));
}
//Contar registros de la tabla
function MostrarContador(itemCount) {
    const name = (itemCount === 1) ? 'Registro' : 'Registros';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

//Método mostrar usuarios
function MostrarUsuarios(data) {
    const tBody = document.getElementById('DetUser');
    tBody.innerHTML = '';

    MostrarContador(data.length);

    const button = document.createElement('button');
    data.forEach(item => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `redirectToEditPage(${item.use_id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteEmple(${item.use_id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let txtCode = document.createTextNode(item.use_id);
        td1.appendChild(txtCode);

        let td2 = tr.insertCell(1);
        let txtIdNumber = document.createTextNode(item.use_idnumber);
        td2.appendChild(txtIdNumber);

        let td3 = tr.insertCell(2);
        let txtNombre = document.createTextNode(item.use_name);
        td3.appendChild(txtNombre);

        let td4 = tr.insertCell(3);
        let txtApellido = document.createTextNode(item.use_lastname);
        td4.appendChild(txtApellido);

        let td5 = tr.insertCell(4);
        let txtUsuario = document.createTextNode(item.use_nickname);
        td5.appendChild(txtUsuario);

        let td6 = tr.insertCell(5);
        let txtPassword = document.createTextNode(item.use_password);
        td6.appendChild(txtPassword);

        let td7 = tr.insertCell(6);
        let txtSexo = document.createTextNode(item.use_gender);
        td7.appendChild(txtSexo);

        let td8 = tr.insertCell(7);
        let txtCorreo = document.createTextNode(item.use_email);
        td8.appendChild(txtCorreo);

        let td9 = tr.insertCell(8);
        let txtTelefono = document.createTextNode(item.use_telephone);
        td9.appendChild(txtTelefono);

        let td10 = tr.insertCell(9);
        let txtRol = document.createTextNode(item.use_rol);
        td10.appendChild(txtRol);

        let td11 = tr.insertCell(10);
        td11.appendChild(editButton);

        let td12 = tr.insertCell(11);
        td12.appendChild(deleteButton);
    });
    todos = data;
}

// Método para obtener los roles y llenar el selector
function obtenerRoles() {
    fetch('/api/tblRols')
        .then(response => {
            if (!response.ok) {
                throw new Error('Error al obtener los roles.');
            }
            return response.json();
        })
        .then(data => {
            const selectRoles = document.getElementById('Rol');

            selectRoles.innerHTML = '<option value="">Seleccione un rol</option>';

            data.forEach(rol => {
                const option = document.createElement('option');
                option.value = rol.rol_id;
                option.text = rol.rol_descripcion;
                selectRoles.appendChild(option);
            });
        })
        .catch(error => console.error('Error al obtener roles:', error.message));
}

obtenerRoles();

function redirectToEditPage(userId) {
    const user = todos.find(item => item.use_id === userId);
    sessionStorage.setItem('editUser', JSON.stringify(user));
    window.location.href = `/tblUser/Edit/${userId}`;
}

//Actualizar usuario
function updateItem() {
    const userCode = document.getElementById('edit_codigo').value;

    const item = {
        use_id: userCode,
        use_idnumber: document.getElementById('edit_identificacion').value.trim(),
        use_name: document.getElementById('edit_nombre').value.trim(),
        use_lastname: document.getElementById('edit_apellido').value.trim(),
        use_nickname: document.getElementById('edit_usuario').value.trim,
        use_password: document.getElementById('edit_password').value.trim(),
        use_gender: document.getElementById('edit_genero').value.trim(),
        use_email: document.getElementById('edit_email').value.trim(),
        use_telephone: document.getElementById('edit_telefono').value.trim(),
        use_rol: document.getElementById('edit_rol').value.trim(),
    };
    fetch(`${uri}/${userCode}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })

        .then(() => getUser())
        .catch(error => console.error('Error al actualizar usuario.', error));
    closeInput();
    return false;
}

//Eliminar usuario
function deleteEmple(Code) {
    fetch(`${uri}/${Code}`, {
        method: 'DELETE'
    })
        .then(() => getUser())
        .catch(error => console.error('Error en eliminar empleado.', error));
}

//Insertar registro de usuario
function addUser() {
    const addIdentificacionTextBox = document.getElementById('Identificacion');
    const addNombreTextBox = document.getElementById('Nombre');
    const addApellidoTextBox = document.getElementById('Apellido');
    const addUsuarioTextBox = document.getElementById('Usuario');
    const addPasswordTextBox = document.getElementById('Password');
    const addGeneroRadioButton1 = document.getElementById('Sexo1');
    const addGeneroRadioButton2 = document.getElementById('Sexo2');
    const addEmailTextBox = document.getElementById('Email');
    const addTelefonoTextBox = document.getElementById('Telefono');
    const addRolTextBox = document.getElementById('Rol');

    let generoValue = '';
    if (addGeneroRadioButton1.checked) {
        generoValue = 'F';
    } else if (addGeneroRadioButton2.checked) {
        generoValue = 'M';
    }

    const item = {
        use_idnumber: addIdentificacionTextBox.value.trim(),
        use_name: addNombreTextBox.value.trim(),
        use_lastname: addApellidoTextBox.value.trim(),
        use_nickname: addUsuarioTextBox.value.trim(),
        use_password: addPasswordTextBox.value.trim(),
        use_gender: generoValue,
        use_email: addEmailTextBox.value.trim(),
        use_telephone: addTelefonoTextBox.value.trim(),
        use_rol: addRolTextBox.value.trim()
    };

    fetch('/api/tblUsers', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Error al agregar usuario.');
            }
            return response.json();
        })
        .then(() => {
            addIdentificacionTextBox.value = '';
            addNombreTextBox.value = '';
            addApellidoTextBox.value = '';
            addUsuarioTextBox.value = '';
            addPasswordTextBox.value = '';
            addEmailTextBox.value = '';
            addTelefonoTextBox.value = '';
            addRolTextBox.value = '';

            getUser();
        })
        .catch(error => console.error('Error al agregar usuario:', error));
}