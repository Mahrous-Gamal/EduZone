async function getData(url = "", ) {
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

    var x = await getData("/api/Numbers");
    var ctx4 = document.getElementById('chart-pie').getContext('2d');
    new Chart(ctx4, {
        type: 'pie',
        data: {
            labels: ["Admin", "Student", "Educator"],
            datasets: [{
                data: [parseInt(x.NumberOfAdmins), parseInt(x.NumberOfStudents), parseInt(x.NumberOfDoctors)],
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

    var x2 = await getData("/api/BatchNumbers");
    console.log(x2);
    var ctx3 = document.getElementById('chart-bar').getContext('2d');
    var b1 = parseInt(x2.Batch1CS) + parseInt(x2.Batch1IS) + parseInt(x2.Batch1IT) ;
    var b2 = parseInt(x2.Batch2CS) + parseInt(x2.Batch2IS) + parseInt(x2.Batch2IT) ;
    var b3 = parseInt(x2.Batch3CS) + parseInt(x2.Batch3IS) + parseInt(x2.Batch3IT) ;
    var b4 = parseInt(x2.Batch4CS) + parseInt(x2.Batch4IS) + parseInt(x2.Batch4IT) ;
    new Chart(ctx3, {
        type: 'bar',
        data: {
            labels: ["Batch1", "Batch2", "Batch3", "Batch4"],
            datasets: [{
                data: [b1, b2, b3, b4],
                label: "Total",
                borderColor: "#3e95cd",
                backgroundColor: "rgb(62,149,205)",
                borderWidth: 2,
                type: 'line',
                fill: false
            }, {
                data: [parseInt(x2.Batch1CS), parseInt(x2.Batch2CS), parseInt(x2.Batch3CS), parseInt(x2.Batch4CS)],
                label: "CS",
                borderColor: "#3cba9f",
                backgroundColor: "#3cba9f",
                borderWidth: 2
            }, {
                data: [parseInt(x2.Batch1IT), parseInt(x2.Batch2IT), parseInt(x2.Batch3IT), parseInt(x2.Batch4IT)],
                label: "IT",
                borderColor: "#ffa500",
                backgroundColor: "#ffa500",
                borderWidth: 2,
            }, {
                data: [parseInt(x2.Batch1IS), parseInt(x2.Batch2IS), parseInt(x2.Batch3IS), parseInt(x2.Batch4IS)],
                label: "IS",
                borderColor: "#c45850",
                backgroundColor: "#c45850",
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

    var x5 = await getData("/api/DataUploadInMonth");
    console.log(x5);
    var ctx1 = document.getElementById("chart-line").getContext("2d");
    new Chart(ctx1, {
        type: "line",
        data: {
            labels: ["Jan", "Feb","Mar","Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
            datasets: [{
                label: "Date Upload",
                tension: 0.4,
                borderWidth: 0,
                pointRadius: 0,
                borderColor: "#cb0c9f",
                borderWidth: 3,
                backgroundColor: gradientStroke1,
                fill: true,
                data: [parseInt(x5.Jan), parseInt(x5.Feb), parseInt(x5.Mar), parseInt(x5.Apr), parseInt(x5.May), parseInt(x5.Jun), parseInt(x5.Jul), parseInt(x5.Aug), parseInt(x5.Sep), parseInt(x5.Oct), parseInt(x5.Nov), parseInt(x5.Dec)],
                maxBarThickness: 6
            }
            ],
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    display: false,
                }
            },
            interaction: {
                intersect: false,
                mode: 'index',
            },
            scales: {
                y: {
                    grid: {
                        drawBorder: false,
                        display: true,
                        drawOnChartArea: true,
                        drawTicks: false,
                        borderDash: [5, 5]
                    },
                    ticks: {
                        display: true,
                        padding: 10,
                        color: '#b2b9bf',
                        font: {
                            size: 11,
                            family: "Open Sans",
                            style: 'normal',
                            lineHeight: 2
                        },
                    }
                },
                x: {
                    grid: {
                        drawBorder: false,
                        display: false,
                        drawOnChartArea: false,
                        drawTicks: false,
                        borderDash: [5, 5]
                    },
                    ticks: {
                        display: true,
                        color: '#b2b9bf',
                        padding: 20,
                        font: {
                            size: 11,
                            family: "Open Sans",
                            style: 'normal',
                            lineHeight: 2
                        },
                    }
                },
            },
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

