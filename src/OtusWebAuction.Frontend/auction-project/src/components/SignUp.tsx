import { Component, type SubmitEvent } from 'react';

// Пропсы компонента: id формы, текст кнопки и т.п.
interface SignUpProps {
    formId?: string;
    buttonText?: string;
}

// Состояние: все поля, которые живут в this.state
interface SignUpState {
    name: string;
    surname: string;
    email: string;
    password: string;
    error: string | null;
}
import { isEmail } from './validation.ts';


class SingUp extends Component<SignUpProps, SignUpState> {
    constructor(props: SignUpProps) {
        super(props);
        this.state = {
            name: '',
            surname: '',
            email: '',
            password: '',
            error: null,
        };

        // Привязываем контекст, чтобы this работал внутри handleSubmit
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    async handleSubmit(event: SubmitEvent<HTMLFormElement>): Promise<void> {
        event.preventDefault();
        if (!isEmail(this.state.email)) {
            this.setState({ error: 'Введите корректный адрес электронной почты' });
            return;
        }

        if (this.state.password.length < 8) {
            this.setState({ error: 'Пароль должен быть не короче 8 символов' });
            return;
        }

        try {
            const response = await fetch('/User/RegisterUser', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    name: this.state.name,
                    surname: this.state.surname,
                    email: this.state.email,
                    password: this.state.password,
                }),
            });
            if (!response.ok) {
                const message: unknown = await response.json().catch(() => null);
                this.setState({
                    error: typeof message === 'string' ? message : 'Не удалось зарегистрироваться',
                });
                return;
            }
            const data: { accessToken: string } = await response.json();
            this.setState({ error: null });
            console.log(data.accessToken);
        } catch {
            this.setState({ error: 'Сервер недоступен' });
        }
    }

    render() {
        return <h1>Привет, {this.props.name}</h1>;
    }
}