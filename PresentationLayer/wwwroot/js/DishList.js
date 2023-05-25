async function AddEatenDish(dishId) {
    const url = "/AddDish";
    const responce = await fetch(url, {
        method: "POST",
        headers: {
            "dishId": dishId
        }
    });

    // Responce handler
    const statusElem = document.querySelector(".responce");
    if (responce.ok) {
        const dishName = await responce.text();
        statusElem.innerText = `${dishName} was added :)`;
    }
    else {
        statusElem.innerText = "Something wrong :(. Reload page or try again later."
    }
}
