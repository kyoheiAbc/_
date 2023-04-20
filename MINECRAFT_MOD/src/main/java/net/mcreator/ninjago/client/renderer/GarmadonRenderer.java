
package net.mcreator.ninjago.client.renderer;

import net.minecraft.resources.ResourceLocation;
import net.minecraft.client.renderer.entity.layers.HumanoidArmorLayer;
import net.minecraft.client.renderer.entity.HumanoidMobRenderer;
import net.minecraft.client.renderer.entity.EntityRendererProvider;
import net.minecraft.client.model.geom.ModelLayers;
import net.minecraft.client.model.HumanoidModel;
import net.mcreator.ninjago.client.model.GarmadonModel;
import net.mcreator.ninjago.entity.GarmadonEntity;

public class GarmadonRenderer extends HumanoidMobRenderer<GarmadonEntity, HumanoidModel<GarmadonEntity>> {
	public GarmadonRenderer(EntityRendererProvider.Context context) {
		super(context, new GarmadonModel(GarmadonModel.LayerDefinitionCreate().bakeRoot()), 0.5f);
		this.addLayer(new HumanoidArmorLayer(this, new HumanoidModel(context.bakeLayer(ModelLayers.PLAYER_INNER_ARMOR)),
				new HumanoidModel(context.bakeLayer(ModelLayers.PLAYER_OUTER_ARMOR))));
	}

	@Override
	public ResourceLocation getTextureLocation(GarmadonEntity entity) {
		return new ResourceLocation("ninjago:textures/entities/garmadon.png");
	}
}
