
Highcharts.chart('SiteAccess', {
    chart: {
        type: 'pie',
        options3d: {
            enabled: true,
            alpha: 45
        }
    },
    title: {
        text: null
    },
   
    plotOptions: {
        pie: {
            innerSize: 100,
            depth: 45
        }
    },
    series: [{
        name: 'Delivered amount',
        data: [
            ['Cardano ', 32],
            ['Cardano', 15],
            ['Cardano', 32],
            ['Cardano', 19]
        ]
    }]
});