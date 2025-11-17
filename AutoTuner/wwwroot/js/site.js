(function () {
    const storageKey = 'autotuner-theme';
    const toggle = document.getElementById('theme-toggle');
    const html = document.documentElement;
    const body = document.body;

    const prefersDark = window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches;
    let theme = localStorage.getItem(storageKey) || (prefersDark ? 'dark' : 'light');

    const applyTheme = (mode) => {
        body.classList.remove('theme-light', 'theme-dark');
        body.classList.add(`theme-${mode}`);
        html.setAttribute('data-bs-theme', mode === 'dark' ? 'dark' : 'light');

        if (toggle) {
            const icon = toggle.querySelector('i');
            if (icon) {
                icon.classList.toggle('fa-moon', mode !== 'dark');
                icon.classList.toggle('fa-sun', mode === 'dark');
            }
            toggle.setAttribute('aria-pressed', mode === 'dark');
            toggle.setAttribute('title', mode === 'dark' ? 'Switch to light mode' : 'Switch to dark mode');
        }
    };

    applyTheme(theme);

    if (toggle) {
        toggle.addEventListener('click', () => {
            theme = theme === 'dark' ? 'light' : 'dark';
            localStorage.setItem(storageKey, theme);
            applyTheme(theme);
        });
    }
})();
