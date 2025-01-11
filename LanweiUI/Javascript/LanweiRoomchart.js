var labelTop = {
    normal: {
        label: {
            show: true,
            position: 'center',
            formatter: '{b}',
            textStyle: {
                baseline: 'bottom'
            }
        },
        labelLine: {
            show: false
        }
    }
};


var labelFromatter = {
    normal: {
        label: {
            formatter: function (a, b, c) { return 100 - c + '%' },
            textStyle: {
                baseline: 'top'
            }
        }
    },
}
var labelBottom = {
    normal: {
        color: '#ccc',
        label: {
            show: true,
            position: 'center'
        },
        labelLine: {
            show: false
        }
    },
    emphasis: {
        color: 'rgba(0,0,0,0)'
    }
};
var radius = [40, 55];