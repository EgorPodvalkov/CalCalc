async function SetCalorieGoal() {
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

    if (responce.ok) {
        location.reload();
    }
    else {
        const shortInfo = document.querySelector(".shortInfo")
        shortInfo.innerHTML += "<div class='.shortInfo-error'>Something wrong with Setting Goal :(, please reload the page and try again.<div>"
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