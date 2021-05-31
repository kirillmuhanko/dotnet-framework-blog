declare global {
    interface Math {
        clamp: (value, min, max) => number;
    }

    interface StringConstructor {
        empty: string;
    }

    interface Window {
        blogApp: any;
        jQuery: any;
    }
}

declare var process : {
    env: {
        NODE_ENV: string
    }
}

export {};