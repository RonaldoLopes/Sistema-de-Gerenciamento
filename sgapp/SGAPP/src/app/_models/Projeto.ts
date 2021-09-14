export interface Projeto {

        id: number;
        codProjeto: string;
        descricao: string;
        recursosPrev: number;
        recursosUtil: number;
        mobilizaPrev: number;
        mobilizaUtili: number;
        dataInicio: Date;
        horasPrevDesen: number;
        horasUtilDesenv: number;
        horasPrevImplement: number;
        horasUtilImplement: number;
        clienteId: number;
        dataConclusao: Date;
        concluido: boolean;
}
