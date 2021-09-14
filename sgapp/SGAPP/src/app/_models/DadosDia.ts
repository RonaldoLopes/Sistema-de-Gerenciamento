import { Time } from '@angular/common';

export interface DadosDia {
    id: number;
    data: Date;
    saidaHotel: Time;
    entraFabrica: Time;
    saidaAlmo: Time;
    retorAlmo: Time;
    saidaLanche: Time;
    retorLanche: Time;
    saidaFabrica: Time;
    chegaHotel: Time;
    atvDia: string;
    interno: boolean;
    horasInterno: Time;
    projetosId: number;
    userId: number;
}
