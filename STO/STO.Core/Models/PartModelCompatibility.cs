namespace STO.Core.Models;

public class PartModelCompatibility
{
    public int PartId { get; private set; }
    public int ModelId { get; private set; }

    // Навигационные свойства
    public Part? Part { get; private set; }
    public Model? Model { get; private set; }

    private PartModelCompatibility(
        int partId,
        int modelId,
        Part? part = null,
        Model? model = null)
    {
        PartId = partId;
        ModelId = modelId;
        Part = part;
        Model = model;
    }

    public static PartModelCompatibility FactoryPartModelCompatibility(
        int partId,
        int modelId,
        Part? part = null,
        Model? model = null)
    {
        if (partId <= 0)
            throw new ArgumentException("Некорректный PartId.");
        if (modelId <= 0)
            throw new ArgumentException("Некорректный ModelId.");
        if (model != null)
        {
            if (model.Id != modelId)
                throw new ArgumentException("Некорректный Model.");
        }
        if (part != null)
        {
            if (part.Id != partId)
                throw new ArgumentException("Некорректный Part.");
        }

        return new PartModelCompatibility(
            partId: partId,
            modelId: modelId,
            part: part,
            model: model
        );
    }
}
