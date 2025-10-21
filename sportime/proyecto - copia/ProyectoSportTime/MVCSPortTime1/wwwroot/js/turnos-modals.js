// JS para abrir Edit/Delete en modales, enviar por AJAX y recargar la grilla (o página) al confirmar.
// Requiere bootstrap.bundle.js (modal).

document.addEventListener('click', function (e) {
    // Edit button
    const editBtn = e.target.closest('.btn-edit');
    if (editBtn) {
        e.preventDefault();
        const url = editBtn.getAttribute('href');
        if (!url) return;

        fetch(url, {
            credentials: 'same-origin',
            headers: { 'X-Requested-With': 'XMLHttpRequest' } // <-- importante para que el controller devuelva PartialView
        })
            .then(r => {
                if (!r.ok) throw new Error('Error loading edit form: ' + r.status);
                return r.text();
            })
            .then(html => {
                const container = document.getElementById('modal-container');
                container.innerHTML = html;
                const modalEl = container.querySelector('.modal');
                if (!modalEl) throw new Error('Partial no contiene .modal');
                const modal = new bootstrap.Modal(modalEl);
                modal.show();

                // intercept form submit inside modal
                const form = container.querySelector('form');
                if (form) {
                    form.addEventListener('submit', function (ev) {
                        ev.preventDefault();
                        const action = form.getAttribute('action') || window.location.href;
                        const formData = new FormData(form);
                        fetch(action, {
                            method: 'POST',
                            body: formData,
                            credentials: 'same-origin',
                            headers: {
                                // Let browser set Content-Type for FormData; indicate AJAX
                                'X-Requested-With': 'XMLHttpRequest'
                            }
                        })
                            .then(r => r.json())
                            .then(json => {
                                if (json && json.success) {
                                    modal.hide();
                                    setTimeout(() => location.reload(), 220);
                                } else {
                                    // Mostrar mensaje de error simple; puedes mejorar para renderizar HTML con errores
                                    alert(json && json.message ? json.message : 'Error al guardar.');
                                }
                            })
                            .catch(err => {
                                console.error(err);
                                alert('Error al guardar (ver consola).');
                            });
                    }, { once: true });
                }
            })
            .catch(err => {
                console.error(err);
                alert('Error al cargar formulario de edición. Revisa la consola/Network.');
            });
        return;
    }

    // Delete button
    const delBtn = e.target.closest('.btn-delete');
    if (delBtn) {
        e.preventDefault();
        const url = delBtn.getAttribute('href');
        if (!url) return;

        fetch(url, {
            credentials: 'same-origin',
            headers: { 'X-Requested-With': 'XMLHttpRequest' } // <-- importante
        })
            .then(r => {
                if (!r.ok) throw new Error('Error loading delete confirmation: ' + r.status);
                return r.text();
            })
            .then(html => {
                const container = document.getElementById('modal-container');
                container.innerHTML = html;
                const modalEl = container.querySelector('.modal');
                if (!modalEl) throw new Error('Partial no contiene .modal');
                const modal = new bootstrap.Modal(modalEl);
                modal.show();

                // intercept delete form submit
                const form = container.querySelector('form');
                if (form) {
                    form.addEventListener('submit', function (ev) {
                        ev.preventDefault();
                        const action = form.getAttribute('action') || window.location.href;
                        const formData = new FormData(form);
                        fetch(action, {
                            method: 'POST',
                            body: formData,
                            credentials: 'same-origin',
                            headers: { 'X-Requested-With': 'XMLHttpRequest' }
                        })
                            .then(r => r.json())
                            .then(json => {
                                if (json && json.success) {
                                    modal.hide();
                                    setTimeout(() => location.reload(), 220);
                                } else {
                                    alert(json && json.message ? json.message : 'Error al eliminar.');
                                }
                            })
                            .catch(err => {
                                console.error(err);
                                alert('Error al eliminar (ver consola).');
                            });
                    }, { once: true });
                }
            })
            .catch(err => {
                console.error(err);
                alert('Error al cargar confirmación de eliminación. Revisa la consola/Network.');
            });
        return;
    }
});