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