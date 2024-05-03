
Highcharts.chart('EquipmentForecast', {
    chart: {
        type: 'area'
    },
    title: {
        text: 'Equipment Forecast Report'
    },
    
    xAxis: {
        allowDecimals: false,
        labels: {
            formatter: function () {
                return this.value; // clean, unformatted number for year
            }
        }
    },
    yAxis: {
        min: 0,
        max: 200,
        title: {
            text: 'null'
        }
       
    },
    
    plotOptions: {
        area: {
            pointStart: 2006,
            marker: {
                enabled: false,
                symbol: 'circle',
                radius: 2,
                states: {
                    hover: {
                        enabled: true
                    }
                }
            }
        }
    },
    series: [{
        name: 'USA',
        color:'#ebebeb',
        data: [6, 11, 32, 10, 35, 100, 140, 132, 110, 150]
    }, {
            name: 'USSR/Russia',
            color: '#71c775',
        data: [
            5, 25, 50, 120, 150, 40, 40, 80, 100, 110]
    }]
});