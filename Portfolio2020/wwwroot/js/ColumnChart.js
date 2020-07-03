﻿function GenerateColumnChart(columns) {
    var chart = AmCharts.makeChart("chartdiv", {
        "type": "serial",
        "theme": "light",
        "dataProvider": columns,
        "valueAxes": [{
            "gridColor": "#FFFFFF",
            "gridAlpha": 0.2,
            "dashLength": 0
        }],
        "gridAboveGraphs": true,
        "startDuration": 1,
        "graphs": [{
            "balloonText": "[[category]]: <b>[[value]]</b>",
            "fillAlphas": 0.8,
            "lineAlpha": 0.2,
            "type": "column",
            "valueField": "dataPoint"
        }],
        "chartCursor": {
            "categoryBalloonEnabled": false,
            "cursorAlpha": 0,
            "zoomable": false
        },
        "categoryField": "name",
        "categoryAxis": {
            "gridPosition": "end",
            "gridAlpha": 0,
            "tickPosition": "end",
            "tickLength": 20
        },
        "export": {
            "enabled": true
        }

    });
}