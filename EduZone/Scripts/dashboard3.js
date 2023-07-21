async function getData(url = "",) {
    const response = await fetch(url, {
        method: "GET",
        mode: "cors",
        credentials: "same-origin",
        cache: "no-cache",
        headers: {
            "Content-Type": "application/json",
        },
        redirect: "follow",
    });
    return response.json();
}
window.onload = async function () {

    var x = await getData("/api/StudentNumbers");
    var ctx4 = document.getElementById('chart-pie').getContext('2d');
    new Chart(ctx4, {
        type: 'pie',
        data: {
            labels: ["GPA", "Total"],
            datasets: [{
                data: [parseInt(x.GPA), 4 - parseInt(x.GPA)],
                borderColor: [
                    "#3cba9f",
                    "#ffa500",
                    "#c45850",
                ],
                backgroundColor: [
                    "rgb(60,186,159,0.1)",
                    "rgb(255,165,0,0.1)",
                    "rgb(196,88,80,0.1)",
                ],
                borderWidth: 2,
            }]
        },
        options: {
            scales: {
                xAxes: [{
                    display: false,
                }],
                yAxes: [{
                    display: false,
                }],
            }
        },
    });

    var x2 = await getData("/api/EducatorLogic1");
    console.log(x2);
    var datekey = [];
    var datevale = [];
    for (const key in x2) {
        datekey.push(key);
        console.log(datekey);
        datevale.push(x2[key]);
        console.log(datevale);
    }
    var ctx3 = document.getElementById('chart-bar').getContext('2d');
    new Chart(ctx3, {
        type: 'bar',
        data: {
            labels: datekey,
            datasets: [{
                data: datevale,
                label: "value",
                borderColor: "#3cba9f",
                backgroundColor: "#3cba9f",
                borderWidth: 2
            }
            ]
        },
    });

    var x3 = await getData("/api/PostNumber");
    var x4 = await getData("/api/PostINGroupNumber");

    var ctx2 = document.getElementById('my-chart').getContext('2d');
    new Chart(ctx2, {
        type: 'line',
        data: {
            labels: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
            datasets: [{
                data: [parseInt(x3.Sunday) + parseInt(x4.Sunday), parseInt(x3.Monday) + parseInt(x4.Monday), parseInt(x3.Tuesday) + parseInt(x4.Tuesday), parseInt(x3.Wednesday) + parseInt(x4.Wednesday), parseInt(x3.Thursday) + parseInt(x4.Thursday), parseInt(x3.Friday) + parseInt(x4.Friday), parseInt(x3.Saturday) + parseInt(x4.Saturday)],
                label: "Total",
                borderColor: "rgb(62,149,205)",
                backgroundColor: "rgb(62,149,205,0.1)",
            }, {
                data: [parseInt(x3.Sunday), parseInt(x3.Monday), parseInt(x3.Tuesday), parseInt(x3.Wednesday), parseInt(x3.Thursday), parseInt(x3.Friday), parseInt(x3.Saturday)],
                label: "TimeLine",
                borderColor: "rgb(60,186,159)",
                backgroundColor: "rgb(60,186,159,0.1)",
            }, {
                data: [parseInt(x4.Sunday), parseInt(x4.Monday), parseInt(x4.Tuesday), parseInt(x4.Wednesday), parseInt(x4.Thursday), parseInt(x4.Friday), parseInt(x4.Saturday)],
                label: "Group",
                borderColor: "rgb(255,165,0)",
                backgroundColor: "rgb(255,165,0,0.1)",
            }
            ]
        },
    });

    var ctx1 = document.getElementById("chart-line").getContext("2d");
    var x3 = await getData("/api/EducatorLogic2");
    console.log(x3);
    var datekey = [];
    var datevale = [];
    for (const key in x3) {
        datekey.push(key);
        console.log(datekey);
        datevale.push(x3[key]);
        console.log(datevale);
    }
    new Chart(ctx1, {
        type: 'bar',
        data: {
            labels: datekey,
            datasets: [{
                data: datevale,
                label: "value",
                borderColor: "#3cba9f",
                backgroundColor: "#3cba9f",
                borderWidth: 2
            }
            ]
        },
    });
}

var ctx1 = document.getElementById("chart-line").getContext("2d");
var gradientStroke1 = ctx1.createLinearGradient(0, 230, 0, 50);
gradientStroke1.addColorStop(1, 'rgba(203,12,159,0.2)');
gradientStroke1.addColorStop(0.2, 'rgba(72,72,176,0.0)');
gradientStroke1.addColorStop(0, 'rgba(203,12,159,0)'); //purple colors
var gradientStroke2 = ctx1.createLinearGradient(0, 230, 0, 50);
gradientStroke2.addColorStop(1, 'rgba(20,23,39,0.2)');
gradientStroke2.addColorStop(0.2, 'rgba(72,72,176,0.0)');
gradientStroke2.addColorStop(0, 'rgba(20,23,39,0)'); //purple colors