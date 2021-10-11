const uri = 'api/pizzas';
let pizzas = [];
var ingredients = [];

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function addInput() {
    let ingredient = document.getElementById('add-List_of_ingredients').value;
    document.getElementById('add-List_of_ingredients').value = '';
    ingredients.push(ingredient);
    let out_arr = document.getElementById('out_arr');
    out_arr.innerHTML = ingredients;
}
function addItem() {
    const addTitleTextbox = document.getElementById('add-Title');
    const addPriceTextbox = document.getElementById('add-Price');
    const addPictureTextbox = document.getElementById('add-Picture');
    const addDescriptionTextbox = document.getElementById('add-Description');
    const addActiveCheckedbox = document.getElementById('add-Active');
    const addNewCheckedbox = document.getElementById('add-New');
    const addDoughTextbox = document.getElementById('add-Dough');
    const addAdditionallyTextbox = document.getElementById('add-Additionally');
    const item = {
        Title: addTitleTextbox.value.trim(),
        Price: addPriceTextbox.value.trim(),
        Picture: addPictureTextbox.value.trim(),
        Description: addDescriptionTextbox.value.trim(),
        Active: addActiveCheckedbox.checked,
        New: addNewCheckedbox.checked,
        Ingredients: ingredients,
        Dough: addDoughTextbox.value.trim(),
        Additionally: addAdditionallyTextbox.value.trim(),
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            addTitleTextbox.value = '';
            addPriceTextbox.value = '';
            addPictureTextbox.value = '';
            ingredients = [];
            addDescriptionTextbox.value = '';
            addAdditionallyTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = pizzas.find(item => item.id === id);

    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-Title').value = item.title;
    document.getElementById('edit-Price').value = item.price;
    document.getElementById('edit-Picture').value = item.picture;
    document.getElementById('edit-Description').value = item.description;
    document.getElementById('edit-Active').checked = item.active;
    document.getElementById('edit-New').checked = item.new;
    document.getElementById('edit-List_of_ingredients').value = item.list_of_ingredients;
    document.getElementById('edit-Dough').value = item.dough;
    document.getElementById('edit-Additionally').value = item.additionally;
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        Title: document.getElementById('edit-Title').value.trim(),
        Price: document.getElementById('edit-Price').value.trim(),
        Picture: document.getElementById('edit-Picture').value.trim(),
        Description: document.getElementById('edit-Description').value.trim(),
        Active: document.getElementById('edit-Active').checked,
        New: document.getElementById('edit-New').checked,
        List_of_ingredients: document.getElementById('edit-List_of_ingredients').value.trim(),
        Dough: document.getElementById('edit-Dough').value.trim(),
        Additionally: document.getElementById('edit-Additionally').value.trim(),
    };

    fetch(`${uri}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const prename = "В каталоге";   
    const postname = (itemCount === 1) ? 'пицца' :
        (itemCount < 5) ? 'пиццы' : 'пицц';

    document.getElementById('counter').innerText = `${prename} ${itemCount} ${postname}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('pizzas');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {

        let ActiveCheckbox = document.createElement('input');
        ActiveCheckbox.type = 'checkbox';
        ActiveCheckbox.disabled = true;
        ActiveCheckbox.checked = item.active;

        let NewCheckbox = document.createElement('input');
        NewCheckbox.type = 'checkbox';
        NewCheckbox.disabled = true;
        NewCheckbox.checked = item.new;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Изменить';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Удалить';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td0 = tr.insertCell(0);
        let Title = document.createTextNode(item.title);
        td0.appendChild(Title);

        let td1 = tr.insertCell(1);
        let Price = document.createTextNode(item.price);
        td1.appendChild(Price);

        let td2 = tr.insertCell(2);
        let Picture = document.createTextNode(item.picture);
        td2.appendChild(Picture);

        let td3 = tr.insertCell(3);
        let Description = document.createTextNode(item.description);
        td3.appendChild(Description);

        let td4 = tr.insertCell(4);
        td4.appendChild(ActiveCheckbox);

        let td5 = tr.insertCell(5);
        td5.appendChild(NewCheckbox);

        let List_of_ingredients = [];
        let td6 = tr.insertCell(6);
        item.ingredients.forEach(i => List_of_ingredients.push(i.title));
        let Ingredients = document.createTextNode(List_of_ingredients);
        td6.appendChild(Ingredients);

        let td7 = tr.insertCell(7);
        let Dough = document.createTextNode(item.dough);
        td7.appendChild(Dough);

        let td8 = tr.insertCell(8);
        let Additionally = document.createTextNode(item.additionally);
        td8.appendChild(Additionally);

        let td9 = tr.insertCell(9);
        td9.appendChild(editButton);

        let td10 = tr.insertCell(10);
        td10.appendChild(deleteButton);
    });

    pizzas = data;
}