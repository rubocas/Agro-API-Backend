console.log("swagger-custom.js carregado e executando!");

function waitForElement(selector, timeout = 5000) {
    return new Promise((resolve, reject) => {
        const intervalTime = 100;
        let elapsedTime = 0;

        const interval = setInterval(() => {
            const element = document.querySelector(selector);
            if (element) {
                clearInterval(interval);
                resolve(element);
            }
            elapsedTime += intervalTime;
            if (elapsedTime >= timeout) {
                clearInterval(interval);
                reject(new Error(`Timeout esperando por elemento: ${selector}`));
            }
        }, intervalTime);
    });
}

(async () => {
    try {
        const swaggerUiDiv = await waitForElement('#swagger-ui');
        const swaggerContainer = await waitForElement('.swagger-container');

        const container = document.createElement('div');
        container.style.margin = '10px';
        container.style.padding = '10px';
        container.style.backgroundColor = '#f0f0f0';
        container.style.border = '1px solid #ddd';
        container.style.borderRadius = '5px';

        container.innerHTML = `
      <input id="login-email" type="text" placeholder="Email" style="margin-right:5px" />
      <input id="login-senha" type="password" placeholder="Senha" style="margin-right:5px" />
      <button id="btn-login">Login</button>
      <span id="login-status" style="margin-left:10px;color:red"></span>
    `;

        swaggerUiDiv.insertBefore(container, swaggerContainer);
        console.log("Formulário inserido no Swagger UI");

        document.getElementById('btn-login').addEventListener('click', async () => {
            const email = document.getElementById('login-email').value;
            const senha = document.getElementById('login-senha').value;
            const status = document.getElementById('login-status');
            status.textContent = '';

            if (!email || !senha) {
                status.textContent = 'Preencha email e senha!';
                return;
            }

            try {
                const response = await fetch('/api/auth/login', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ email: email, senha: senha })
                });

                if (!response.ok) {
                    status.textContent = 'Login falhou!';
                    return;
                }

                const data = await response.json();
                if (!data.token) {
                    status.textContent = 'Token não encontrado na resposta!';
                    return;
                }

                const bearerToken = 'Bearer ' + data.token;
                if (window.ui && window.ui.preauthorizeApiKey) {
                    window.ui.preauthorizeApiKey('Bearer', bearerToken);
                    status.style.color = 'green';
                    status.textContent = 'Login OK! Token aplicado.';
                    console.log("Token aplicado no Swagger UI");
                } else {
                    status.textContent = 'Swagger UI não está pronto.';
                    console.error("window.ui ou preauthorizeApiKey não está disponível");
                }
            } catch (err) {
                status.textContent = 'Erro no login: ' + err.message;
                console.error(err);
            }
        });
    } catch (err) {
        console.error(err.message);
    }
})();