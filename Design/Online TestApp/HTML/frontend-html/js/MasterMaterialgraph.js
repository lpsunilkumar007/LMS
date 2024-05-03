
Highcharts.chart('MasterMaterial', {
    chart: {
        type: 'bar'
    },
    title: {
        text: 'Master Material Report'
    },
    xAxis: {
        categories: [
            'Jan',
            'Feb',
            'Mar',
            'Apr',
            'May',
            'Jun',
            'Jul',
            'Aug',
            'Sep',
            'Oct',
            'Nov',
            'Dec'
        ],
        title: {
            text: null
        }
    },
    yAxis: {
        min: 0,
        title: {
            text: 'null',
            align: 'high'
        },
        labels: {
            overflow: 'justify'
        }
    },
    tooltip: {
        valueSuffix: ' null'
    },
    plotOptions: {
        bar: {
            dataLabels: {
                enabled: true
            }
        }
    },
   
    credits: {
        enabled: false
    },
    series: [{
        name: 'Jan',
        data: [20, null, null, null, null, null, null, null, null, null, null, null]
    }, {
        name: 'Feb',
            data: [null, 10, null, null, null, null, null, null, null, null, null, null]
        }, {
            name: 'March',
            data: [null, null, 15, null, null, null, null, null, null, null, null, null]
    }, {
            name: 'April',
            data: [null, null, null, 25, null, null, null, null, null, null, null, null]
        }, {
            name: 'May',
            data: [null, null, null, null, 45, null, null, null, null, null, null, null]
    }, {
            name: 'June',
            data: [null, null, null, null, null, 05, null, null, null, null, null, null]
        }, {
            name: 'July',
            data: [null, null, null, null, null, null, 35, null, null, null, null, null]
    }, {
            name: 'Aug',
            data: [null, null, null, null, null, null, null, 10, null, null, null, null]
        }, {
            name: 'Sept',
            data: [null, null, null, null, null, null, null, null, 12, null, null, null]
    }, {
            name: 'Oct',
            data: [null, null, null, null, null, null, null, null, null, 8, null, null]
        }, {
            name: 'Nov',
            data: [null, null, null, null, null, null, null, null, null, null, 13, null]
    }, {
            name: 'Dec',
            data: [null, null, null, null, null, null, null, null, null, null, null, 20]
    }]
});