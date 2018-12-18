window.onload = function () {
    const output = document.getElementById("calculator-out");
    const input = document.getElementById('calculator-keys');

    input.addEventListener('click', e => {
        let n = e.target.innerHTML;

        if (n >= 0 || n <= 9) {
            addNumber(n);
        }

        switch (n) {
            case 'C': reset(); break;
            case '+': addOperator('+'); break;
            case '-': addOperator('-'); break;
            case '*': addOperator('*'); break;
            case '/': addOperator('/'); break;
            case '=':
                try {
                    output.value += `= ${eval(output.value)}`;
                }
                catch (e) {
                    output.value = e;
                }
                reset();
                return;
        }

        output.value = '';
        for (i = 0; i < num.length; i++) {
            output.value += `${num[i]} `;
            if (op[i] !== undefined) {
                output.value += `${op[i]} `;
            }
        }
    });

    reset();
}

var num, op;
function reset() {
    num = [];
    op = [];
}

function addNumber(number) {
    if (num.length == 0 || num.length == op.length) {
        // New number
        num.push(number);
    }
    else {
        // Concat
        num[num.length - 1] += number;
    }
}

function addOperator(operator) {
    if (op.length == num.length - 1) {
        // New operator
        op.push(operator);
    }
    else {
        // Change operator
        op[op.length - 1] = operator;
    }
}
