    document.addEventListener('DOMContentLoaded', function() {
    const rows = document.querySelectorAll('.table-row');

    rows.forEach(row => {
    row.addEventListener('mouseenter', function() {
    row.querySelector('.action-buttons').style.display = 'table-row';
});

    row.addEventListener('mouseleave', function() {
    row.querySelector('.action-buttons').style.display = 'none';
});
});
});
