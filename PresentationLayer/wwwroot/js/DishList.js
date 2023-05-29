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
        const dishes = await responce.json();
        // Generating Body of Table
        if (dishes.length == 0) {
            statusElem.innerText = "No dishes matching your filter.";
        }
        else { 
            const tableElem = document.querySelector(".table_body");
            tableElem.innerHTML = "";
            for (let index = 0; index < dishes.length; index++) {
                const dish = dishes[index];
                tableElem.innerHTML += `<tr>
                    <th>${dish.name}</th>
                    <td>${dish.kCalorie} KCal</td>
                    <td>${dish.totalFat} / ${dish.protein} / ${dish.carbohydrates}</td>
                    <td><input class="adding_button" type="button" value="Add to Eaten" onclick="AddEatenDish(${dish.id})"/></td>
                    </tr>`;
            }
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
