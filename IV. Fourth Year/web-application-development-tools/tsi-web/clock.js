//$(document).ready(function () {
$(function () {
    // Clock dials
    for (let i = 1; i < 13; i++) {
        $(document.createElementNS('http://www.w3.org/2000/svg', 'line')).attr({
            x1: '50%', y1: '18%',
            x2: '50%', y2: '10%',
            'transform-origin': '50% 50%',
            'transform': `rotate(${(i / 12) * 360})`,
            style: `stroke:#3E92CC; stroke-width:${i % 2 == 0 ? 2 : 1}`,
        }).appendTo("#section-jquery svg");
    }

    $(document.createElementNS('http://www.w3.org/2000/svg', 'circle')).attr({
        cx: '50%', cy: '50%', r: '45%',
        stroke: 'black', 'stroke-width': 3, fill: 'none',
    }).appendTo("#section-jquery svg");

    // Arrows
    $(document.createElementNS('http://www.w3.org/2000/svg', 'line')).attr({
        id: 'clock-h',
        x1: '50%', y1: '50%',
        x2: '50%', y2: '20%',
        'stroke-linecap': 'round',
        'transform-origin': '50% 50%',
        style: 'stroke:black; stroke-width:5',
    }).appendTo("#section-jquery svg");

    $(document.createElementNS('http://www.w3.org/2000/svg', 'line')).attr({
        id: 'clock-m',
        x1: '50%', y1: '50%',
        x2: '50%', y2: '15%',
        'stroke-linecap': 'round',
        'transform-origin': '50% 50%',
        style: 'stroke:black; stroke-width:3',
    }).appendTo("#section-jquery svg");

    $(document.createElementNS('http://www.w3.org/2000/svg', 'line')).attr({
        id: 'clock-s',
        x1: '50%', y1: '50%',
        x2: '50%', y2: '10%',
        'stroke-linecap': 'round',
        'transform-origin': '50% 50%',
        style: 'stroke:red; stroke-width:2',
    }).appendTo("#section-jquery svg");

    // Center
    $(document.createElementNS('http://www.w3.org/2000/svg', 'circle')).attr({
        cx: '50%', cy: '50%', r: '2%',
        stroke: 'black', 'stroke-width': 2, fill: 'red',
    }).appendTo("#section-jquery svg");

    setInterval(tick, 1000);
});

function tick() {
    let date = new Date(); // Calculate
    let hd = (date.getHours() / 12) * 360 + (date.getMinutes() / 2);
    let md = (date.getMinutes() / 60) * 360 + (date.getSeconds() / 10);
    let sd = (date.getSeconds() / 60) * 360; //+ (date.getMilliseconds() / 160);

    // Rotate
    $('#clock-h').attr('transform', `rotate(${hd})`);
    $('#clock-m').attr('transform', `rotate(${md})`);
    $('#clock-s').attr('transform', `rotate(${sd})`);
}
