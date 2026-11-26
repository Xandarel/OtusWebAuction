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
