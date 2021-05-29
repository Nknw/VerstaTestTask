(function () {
    'use strict'

    let forms = document.querySelectorAll('.needs-validation');

    let tbody = document.querySelector('tbody');

    fetch("/api/v1/orders", {
        method: "GET"
    }).then(async res => {
        if (res.ok) {
            await res.json().then(orders => orders.forEach(appendOrder));
        } else {
            somethingGoesWrong('неудалось загрузить заказы');
        }
    }).catch(_ => somethingGoesWrong('неудалось загрузить заказы'));

    Array.from(document.querySelectorAll('label')).map(label => {
        return {
            label: label,
            div: label.parentElement
        };
    }).filter(obj => obj.div.classList.contains('default-feedback'))
        .forEach(obj => {
            let div = document.createElement('div');
            div.classList.add('invalid-feedback');
            div.innerText = `Пожалуйста, заполните ${obj.label.innerText.toLowerCase()}`;
            obj.div.appendChild(div);
        });

    forms.forEach(form => {
        form.addEventListener('submit', function (event) {
            event.preventDefault()
            event.stopPropagation()
            if (!form.checkValidity()) {
                form.classList.add('was-validated');
                return;
            }
            fetch("/api/v1/orders", {
                method: "POST",
                body: new FormData(event.target)
            }).then(async res => {
                if (res.ok) {
                    form.reset();
                    await res.json().then(order => appendOrder(order));
                } else {
                    somethingGoesWrong("неудалось создать заказ");
                }
            }).catch(_ => somethingGoesWrong('неудалось создать заказ'));
        }, false)
    });

    function somethingGoesWrong(errorMessage) {
        let p = document.createElement('p');
        p.innerText = `Что-то пошло не так, ${errorMessage}, попробуйте позже`;
        document.querySelector('.container').prepend(p);
    }

    function appendOrder(order) {
        let tr = document.createElement('tr');
        ['id', 'senderCity', 'senderAddress', 'recipientCity', 'recipientAddress', 'weight', 'shipmentDate'].forEach(key => {
            let td = document.createElement('td');
            td.innerText = order[key];
            tr.appendChild(td);
        });
        tbody.prepend(tr);
    }
})()
