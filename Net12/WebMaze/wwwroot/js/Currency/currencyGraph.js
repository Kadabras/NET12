﻿$(document).ready(function () {

    const paramsUrl = new URLSearchParams(window.location.search);
    let index = 0;
    let currencyIds = [];
    while (true) {
        let currencyId = paramsUrl.get(`currencyId[${index}]`);
        if (currencyId === null) {
            break;
        }
        currencyIds.push(currencyId);
        index++;
    }
    const onStartDate = paramsUrl.get("onStartDate");
    const onEndDate = paramsUrl.get("onEndDate");

    let url = "/Currency/GetRateByIdOnPeriodJson?"
    for (var i = 0; i < currencyIds.length; i++) {
        let cur_id = `currencyId[${i}]=${currencyIds[i]}&`;
        url = url + cur_id;
    }
    url = url + `onStartDate=${onStartDate}&onEndDate=${onEndDate}`;

    $.get(url)
        .done(function (arrayOfrateList) {
            let rateList = [];
            let dates = [];

            for (var i = 0; i < arrayOfrateList.length; i++) {
                let ratesArr = [];
                rateList.push(ratesArr);
                for (var j = 0; j < arrayOfrateList[i].length; j++) {
                    let rate = arrayOfrateList[i][j].cur_OfficialRate;
                    rateList[i].push(rate);
                }
            }
            let index = 0;
            for (var j = 0; j < arrayOfrateList[index].length; j++) {
                let date = arrayOfrateList[index][j].date;
                dates.push(date);
            }

            class datasetFromService {
                constructor(label, data) {
                    this.label = label;
                    this.data = data;
                };
                fill = false;
                borderColor = 'rgb(75, 192, 192)';
                tension = 0.1;
            }

            let listOfdatasets = [];
            for (var i = 0; i < rateList.length; i++) {
                let dataset = new datasetFromService(`Rate${i}`, rateList[i]);
                listOfdatasets.push(dataset);
            }

            const data = {
                labels: dates,
                datasets: listOfdatasets,
            };

            const ctx = document.getElementById('myChart').getContext('2d');
            const myChart = new Chart(ctx, {
                type: 'line',
                data: data,
                options: {
                    responsive: true,
                }
            });
        });
});