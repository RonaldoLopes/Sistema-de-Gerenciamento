import { Time } from '@angular/common';

export interface Ponto {

    id: number;
    data: Date;
    entraFabrica: Time;
    saidaAlmo: Time;
    retorAlmo: Time;
    saidaFabrica: Time;
    atvDia: string;
    projetosId: number;
    userId: number;
}
