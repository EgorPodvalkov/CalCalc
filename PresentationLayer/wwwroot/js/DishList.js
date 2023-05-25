async function AddEatenDish(dishId) {
    const statusElem = document.querySelector(".responce");
    statusElem.innerText = "";

    const url = "/AddDish";
    const responce = await fetch(url, {
        method: "POST",
        headers: {
            "dishId": dishId
        }
    });

    // Responce handler
    if (responce.ok) {
        const dishName = await responce.text();
        statusElem.innerText = `${dishName} was added :)`;
    }
    else {
        statusElem.innerText = "Something wrong :(. Reload page or try again later.";
    }
}

async function GetDishes() {
    // Clearing status
    const statusElem = document.querySelector(".status");
    statusElem.innerText = "";

    // Getting filter info
    const searchElem = document.querySelector(".search");
    const minCalElem = document.querySelector(".min-calorie");
    const maxCalElem = document.querySelector(".max-calorie");
    const filter = {
        "Search": searchElem.value,
        "MinCalorie": parseInt(minCalElem.value, 10),
        "MaxCalorie": parseInt(maxCalElem.value, 10)
    }
    const jsonFilter = JSON.stringify(filter);

    // Query generating
    const url = "/GetDishes";
    const responce = await fetch(url, {
        method: "POST",
        headers: {
            "filter": jsonFilter
        }
    });

    // Responce handler
    if (responce.ok) {
        const tableHead = `
        <tr class="tableHead">
            <td>Name</td>
            <td>KCalories</td>
            <td>Fat / Proteins / Carbongidrates</td>
        </tr>`;

        const tableElem = document.querySelector(".table");
        const dishes = await responce.json();
        

        // Adding Head of Table
        tableElem.innerHTML = tableHead;

        // Generating Body of Table
        if (dishes.length == 0) {
            statusElem.innerText = "No dishes matching your filter.";
        }

        for (let index = 0; index < dishes.length; index++) {
            const dish = dishes[index];
            tableElem.innerHTML += `<tr>
            <td>${dish.name}</td>
            <td>${dish.kCalorie} kcal</td>
            <td>${dish.totalFat} / ${dish.protein} / ${dish.carbohydrates}</td>
            <td><input type="button" value="Add to Eaten" onclick="AddEatenDish(${dish.Id})"/></td>
            <tr>`
        }
    }
    else {
        const errorElem = document.querySelector(".responce");
        errorElem.innerText = "Something wrong :(. Reload page or try again later.";
    }
}

const searchElem = document.querySelector(".search");
const minCalElem = document.querySelector(".min-calorie");
const maxCalElem = document.querySelector(".max-calorie");

searchElem.oninput = function () { GetDishes(); }
minCalElem.oninput = function () { GetDishes(); }
maxCalElem.oninput = function () { GetDishes(); }
