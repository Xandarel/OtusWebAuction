import {type ChangeEvent, Component, type SubmitEvent} from 'react';
import { Button } from "@/components/ui/button"
import {
    Field,
    FieldDescription,
    FieldGroup,
    FieldLabel,
} from "@/components/ui/field"
import { Input } from "@/components/ui/input"


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
    emailError: string | null;
    passwordError: string | null;
}
import { apiUrl } from '../api.ts';
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
            emailError: null,
            passwordError: null,
        };

        // Привязываем контекст, чтобы this работал внутри handleSubmit
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(event: ChangeEvent<HTMLInputElement>) {
        const { name, value } = event.target;
        switch (name) {
            case 'name':
                this.setState({ name: value });
                break;
            case 'surname':
                this.setState({ surname: value });
                break;
            case 'email':
                this.setState({
                    email: value,
                    emailError: value.trim() !== '' && !isEmail(value)
                        ? 'Введите корректный адрес электронной почты'
                        : null,});
                break;
            case 'password':
                this.setState({
                    password: value,
                    passwordError: value !== '' && value.length < 8
                        ? 'Пароль должен быть не короче 8 символов'
                        : null,
                });
                break;
        }
    }

    async handleSubmit(event: SubmitEvent<HTMLFormElement>): Promise<void> {
        event.preventDefault();
        const emailError = !isEmail(this.state.email)
            ? 'Введите корректный адрес электронной почты'
            : null;
        const passwordError = this.state.password.length < 8
            ? 'Пароль должен быть не короче 8 символов'
            : null;
        if (emailError !== null || passwordError !== null) {
            this.setState({ emailError, passwordError });
            return;
        }

        try {
            const response = await fetch(apiUrl('/User/RegisterUser'), {
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
        return <>
            <form className="w-full max-w-sm" onSubmit={this.handleSubmit}>
                <FieldGroup>
                    <Field>
                        <FieldLabel htmlFor="name">
                            Имя<span className="text-destructive">*</span>
                        </FieldLabel>
                        <Input
                            id="name"
                            name="name"
                            type="text"
                            placeholder="Иван"
                            value={this.state.name}
                            onChange={this.handleChange}
                            required
                        />
                    </Field>
                    <Field>
                        <FieldLabel htmlFor="surname">
                            Фамилия<span className="text-destructive">*</span>
                        </FieldLabel>
                        <Input
                            id="surname"
                            name="surname"
                            type="text"
                            placeholder="Иванов"
                            value={this.state.surname}
                            onChange={this.handleChange}
                            required
                        />
                    </Field>
                    <Field>
                        <FieldLabel htmlFor="form-email">
                            Email<span className="text-destructive">*</span>
                        </FieldLabel>
                        <Input
                            type="email"
                            name="email"
                            id="email"
                            placeholder="john@example.com"
                            value={this.state.email}
                            onChange={this.handleChange}
                            aria-invalid={this.state.emailError !== null}
                            aria-describedby={this.state.emailError !== null ? 'email-error' : undefined}
                        />
                        <FieldDescription>
                            {this.state.emailError !== null && <p id="email-error">{this.state.emailError}</p>}
                        </FieldDescription>
                    </Field>
                    <Field>
                        <FieldLabel htmlFor="password">
                            Пароль<span className="text-destructive">*</span>
                        </FieldLabel>
                        <Input
                            type="password"
                            name="password"
                            id="password"
                            value={this.state.password}
                            onChange={this.handleChange}
                            aria-invalid={this.state.passwordError !== null}
                            aria-describedby={this.state.passwordError !== null ? 'password-error' : undefined}
                            required
                        />
                        <FieldDescription>
                            {this.state.passwordError !== null && <p id="password-error">{this.state.passwordError}</p>}
                        </FieldDescription>
                    </Field>

                </FieldGroup>
                <Field orientation="horizontal">
                    <Button type="button" variant="outline">
                        Отмена
                    </Button>
                    <Button type="submit">Регистрация</Button>
                </Field>
            </form>
        </>
    }
}
export default SingUp;