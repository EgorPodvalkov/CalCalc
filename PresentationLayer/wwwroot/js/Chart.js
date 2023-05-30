async function GenerateChart(days = 7) {
    console.log("da");
    const url = `/GetChartInfo/${days}`;
    const responce = await fetch(url, {
        method: "GET"
    })

    // Responce Handler
    if (responce.ok) {
        // Getting info
        const chartInfo = await responce.json();
        const dates = chartInfo.dates;
        const calorieGoals = chartInfo.calorieGoals;
        const realCalories = chartInfo.realCalories;
        console.log(chartInfo);

        new Chart("calorieChart", {
            type: "line",
            data: {
                labels: dates,
                datasets: [{
                    label: "Calorie Goal",
                    backgroundColor: "blue",
                    borderColor: "blue",
                    data: calorieGoals,
                    fill: false
                }, {
                    label: "Real Calorie",
                    backgroundColor: "green",
                    borderColor: "green",
                    data: realCalories,
                    fill: false
                }
                ]
            },
            options: {
                interaction: {
                    mode: 'index',
                    intersect: false,
                },
                scales: {
                    y: {
                        display: true,
                        title: {
                            display: true,
                            text: "Calories, KCal",
                        },
                        min: 0
                    },
                },
            },
        });
    }
    else {
        const errorElem = document.querySelector(".error");
        errorElem.innerText = "Something wrong with servers :(";
    }
}

GenerateChart();
