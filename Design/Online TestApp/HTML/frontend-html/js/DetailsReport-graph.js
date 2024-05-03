
Highcharts.chart('DetailsReport', {
    chart: {
        type: 'column'
    },
    title: {
        text: 'Project Details Report'
    },
    
    xAxis: {
        categories: [
            'Day 1',
            'Day 2',
            'Day 3',
            'Day 4',
            'Day 5',
            'Day 6',
            'Day 7'
        ],
        crosshair: true
    },
    yAxis: {
        min: 0,
        max: 100,
       title: {
            text: null
        }    
    },
    
    plotOptions: {
        column: {
            pointPadding: 0.2,
            borderWidth: 0
        }
    },
    series: [{
        name: 'Page View',
        color: '#71c775',
        data: [49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6]
        
    }, {
            name: 'Page Search',
            color: '#25a8e0',
        data: [83.6, 78.8, 98.5, 93.4, 106.0, 84.5, 105.0]

    }, {
            name: 'Page click',
            color:'#dcdcdc',
        data: [48.9, 38.8, 39.3, 41.4, 47.0, 48.3, 59.0]

    }]
});