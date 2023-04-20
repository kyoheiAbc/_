package net.ninjago.playerMoel;

import net.minecraft.client.renderer.entity.LivingEntityRenderer;
import net.minecraftforge.client.event.EntityRenderersEvent;
import net.minecraftforge.client.event.RenderPlayerEvent;
import net.minecraftforge.common.MinecraftForge;
import net.minecraftforge.eventbus.api.SubscribeEvent;
import net.minecraftforge.fml.common.Mod;

@Mod.EventBusSubscriber(bus = Mod.EventBusSubscriber.Bus.MOD)
public class event {

    public event() {
        MinecraftForge.EVENT_BUS.register(this);
    }

    @SubscribeEvent
    public void renderPlayerEventPre(RenderPlayerEvent.Pre e) {
        e.getRenderer().getModel().setAllVisible(false);
        // e.getPoseStack().pushPose();
        // e.getPoseStack().scale(2.0f, 2.0f, 2.0f);
    }

    @SubscribeEvent
    public void renderPlayerEventPost(RenderPlayerEvent.Post e) {
        // e.getPoseStack().popPose();
    }

    @SubscribeEvent
    public static void entityRenderersEventAddLayers(EntityRenderersEvent.AddLayers e) {
        LivingEntityRenderer livingEntityRenderer;

        livingEntityRenderer = (LivingEntityRenderer) e.getSkin("default");
        livingEntityRenderer.addLayer(new renderer(livingEntityRenderer));

        livingEntityRenderer = (LivingEntityRenderer) e.getSkin("slim");
        livingEntityRenderer.addLayer(new renderer(livingEntityRenderer));
    }
}
