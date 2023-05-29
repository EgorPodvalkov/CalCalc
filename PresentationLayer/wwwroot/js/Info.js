async function SetCalorieGoal() {
    // Error clearing
    const errElem = document.querySelector(".shortInfo-error");
    errElem.innerText = "";

    // Sending query
    let goal = document.querySelector(".shortInfo-goalInput").value;
    if (goal == "")
        goal = 0;
    const url = `/SetGoal`;
    const responce = await fetch(url, {
        method: 'PUT',
        headers: {
            'goal': goal
        }
    });

    // Responce handler
    if (responce.ok) {
        const goalElem = document.querySelector(".shortInfo-goal");
        const buttonElem = document.querySelector(".shortInfo-goalButton");
        const goal = await responce.text();
        console.log(goal);
        if (goal == 0) {
            goalElem.innerHTML = " kcal";
            buttonElem.value = "Add Calorie Goal";
        }
        else {
            goalElem.innerHTML = ` / ${goal} kcal`;
            buttonElem.value = "Change Calorie Goal";
        }
    }
    else {
        const errElem = document.querySelector(".shortInfo-error");
        errElem.innerText = "Something wrong with Setting Goal :(, please reload the page and try again.";
    }
}

document.addEventListener('keydown', (event) => {
    if (event.code == "Enter") {
        const input = document.querySelector(".shortInfo-goalInput");
        if (document.activeElement === input) {
            SetCalorieGoal();
        }
    }
});

async function GetUserInfo() {
    const url = `/GetUserInfo`;
    const responce = await fetch(url, {
        method: 'GET'
    });

    // Responce Handler
    if (responce.ok) {
        const info = await responce.json();

        const calorie = document.querySelector(".shortInfo-KCalorie");
        const tableBody = document.querySelector(".table_body");

        // Calorie
        calorie.innerText = info.kCalorieReal;

        // Dishes
        const dishes = info.eatenDishes;
        if (dishes.length == 0) {
            tableBody.innerHTML =
                `<tr>
                    <th>No Dishes Eaten Yet :|</th>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>`;
        }
        else {
            tableBody.innerHTML = "";
            for (let i = 0; i < dishes.length; i++) {
                tableBody.innerHTML +=
                    `<tr class="alert" role="alert">
                        <th scope="row">${dishes[i].name}</th>
                        <td>${dishes[i].kCalorie * dishes[i].quantity} KCal</td>
                        <td>${dishes[i].quantity}</td>
                        <td><input class="removing_button" type="button" value="Remove 1" onclick="RemoveDish(${i})"></td>
                    </tr>`;
            }
        }
    }
    else {
        const tableBody = document.querySelector(".table_body");
        tableBody.innerHTML =
            `<tr>
                <th>Something Wrong with Getting info :(</th>
                <td></td>
                <td></td>
                <td></td>
            </tr>`;
    }
}

async function RemoveDish(index) {
    const errElem = document.querySelector(".error");
    errElem.innerText = "";

    const url = `/RemoveDish/${index}`;
    const responce = await fetch(url, {
        method: 'DELETE'
    });

    // Responce Handler
    if (responce.ok) {
        await GetUserInfo();
    }
    else {
        errElem.innerText = "Something Wrong with Removing. Try to reload page.";
    }
}