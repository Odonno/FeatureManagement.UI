import * as hooks from 'preact/hooks';
import merge from 'mergerino';
import { AuthenticationScheme, Feature } from '../models';
const staterino = require('staterino');

export type State = {
    authSchemes?: AuthenticationScheme[];
    selectedAuthScheme?: AuthenticationScheme;
    features?: Feature[];
};

const state: State = {};

const useStore = staterino.default({ merge, hooks, state });

const { set, get, subscribe } = useStore;

export const setAuthSchemes =
    (authSchemes?: AuthenticationScheme[]) => set({ authSchemes });
export const setSelectedAuthScheme =
    (authScheme?: AuthenticationScheme) => set({ selectedAuthScheme: authScheme });
export const setFeatures =
    (features?: Feature[]) => set({ features });

export const useAuthSchemes: () => AuthenticationScheme[] =
    () => useStore((s: State) => s.authSchemes);
export const useSelectedAuthScheme: () => AuthenticationScheme =
    () => useStore((s: State) => s.selectedAuthScheme);
export const useFeatures: () => Feature[] =
    () => useStore((s: State) => s.features);

export const useAuthSelectionEnabled: () => boolean =
    () => useStore((s: State) => s.authSchemes && s.authSchemes.length > 0 && s.authSchemes.some(as => as.type !== 'None'));