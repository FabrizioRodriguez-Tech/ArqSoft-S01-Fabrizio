/* ============================================================
   VALORANT-STYLE INTERACTIVITY — site.js
   ============================================================ */

document.addEventListener('DOMContentLoaded', () => {
    'use strict';

    // 1. Efecto de Sonido "Click" Táctico (Opcional)
    // Agrega un pequeño feedback sonoro a los botones principales
    const playClickSound = () => {
        // Solo si decides añadir un audio corto
        // new Audio('/path-to-sound.mp3').play();
    };

    // 2. Animación de Entrada para Cards
    const observerOptions = {
        threshold: 0.1,
        rootMargin: "0px 0px -50px 0px"
    };

    const cardObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.style.opacity = "1";
                entry.target.style.transform = "translateY(0)";
                cardObserver.unobserve(entry.target);
            }
        });
    }, observerOptions);

    document.querySelectorAll('.card').forEach(card => {
        card.style.opacity = "0";
        card.style.transform = "translateY(20px)";
        card.style.transition = "all 0.4s cubic-bezier(0.16, 1, 0.3, 1)";
        cardObserver.observe(card);
    });

    // 3. Efecto de Glitch sutil en botones Primary al hacer hover
    const primaryBtns = document.querySelectorAll('.btn-primary');
    primaryBtns.forEach(btn => {
        btn.addEventListener('mouseenter', () => {
            btn.style.letterSpacing = "0.15em";
        });
        btn.addEventListener('mouseleave', () => {
            btn.style.letterSpacing = "0.1em";
        });
    });

    // 4. Navbar Dynamic Transparency
    // Hace que la navbar sea más sólida al hacer scroll
    const navbar = document.querySelector('.navbar');
    window.addEventListener('scroll', () => {
        if (window.scrollY > 50) {
            navbar.style.backgroundColor = 'rgba(15, 25, 35, 0.98)';
            navbar.style.padding = '0.5rem 1rem';
        } else {
            navbar.style.backgroundColor = 'rgba(15, 25, 35, 0.96)';
            navbar.style.padding = '0.8rem 1rem';
        }
    });

    // 5. Validación de Formularios (Estilo Valorant)
    const forms = document.querySelectorAll('.was-validated');
    Array.from(forms).forEach(form => {
        form.addEventListener('submit', event => {
            if (!form.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();

                // Efecto de sacudida (shake) si falla
                form.classList.add('val-shake');
                setTimeout(() => form.classList.remove('val-shake'), 400);
            }
            form.classList.add('was-validated');
        }, false);
    });

    // 6. Tooltips y Popovers de Bootstrap
    // Necesario para que funcionen los componentes de Bootstrap 5
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // 7. Input Focus Log (Consola estética)
    // Imprime en consola un mensaje "técnico" cuando el usuario interactúa
    const inputs = document.querySelectorAll('.form-control');
    inputs.forEach(input => {
        input.addEventListener('focus', () => {
            console.log(`%c > ACCESSING_MODULE: ${input.name || 'INPUT_FIELD'}...`, 'color: #ff4655; font-weight: bold;');
        });
    });
});

/* ── Estilos extra para animaciones de JS ────────────────── */
const style = document.createElement('style');
style.textContent = `
    .val-shake {
        animation: valShake 0.4s cubic-bezier(.36,.07,.19,.97) both;
    }
    @keyframes valShake {
        10%, 90% { transform: translate3d(-1px, 0, 0); }
        20%, 80% { transform: translate3d(2px, 0, 0); }
        30%, 50%, 70% { transform: translate3d(-4px, 0, 0); }
        40%, 60% { transform: translate3d(4px, 0, 0); }
    }
`;
document.head.appendChild(style);