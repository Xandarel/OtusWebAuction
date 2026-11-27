const TOKEN_KEY = 'accessToken';

function apiBaseUrl(): string {
    const configured = import.meta.env.VITE_API_BASE_URL;
    if (!configured) {
        throw new Error('VITE_API_BASE_URL is not set');
    }

    return configured.replace(/\/$/, '');
}

export function apiUrl(path: string): string {
    const normalized = path.startsWith('/') ? path : `/${path}`;
    return `${apiBaseUrl()}${normalized}`;
}

export function authHeaders(extra?: HeadersInit): Headers {
    const headers = new Headers(extra);
    const token = localStorage.getItem(TOKEN_KEY);
    if (token) {
        headers.set('Authorization', `Bearer ${token}`);
    }
    return headers;
}
